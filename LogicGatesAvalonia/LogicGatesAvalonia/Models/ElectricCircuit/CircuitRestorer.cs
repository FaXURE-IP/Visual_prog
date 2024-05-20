using Avalonia.Media;
using LogicGatesAvalonia.Controls;
using LogicGatesAvalonia.Models.DataBaseModel;
using LogicGatesAvalonia.Models.LogicalGatesFactory;
using LogicGatesAvalonia.Models.LogicalGatesFactory.FactoryExceptions;
using LogicGatesAvalonia.ViewModels;
using LogicGatesAvinternal.Controls;
using System;
using System.Collections.Generic;

namespace LogicGatesAvalonia.Models.ElectricCircuit
{
    public class CircuitRestorer
    {
        private Dictionary<int, BaseLogicalGateControl> hashCodeToControlDict = 
            new Dictionary<int, BaseLogicalGateControl>();

        private MainWindowViewModel appMainViewModel;
        public CircuitRestorer(MainWindowViewModel applicationMainViewModel) 
        {
            this.appMainViewModel = applicationMainViewModel;
        }

        bool wasRestored = false;

        public void Restore()
        {
            if (!wasRestored)
            {
                RestoreLogicGates();
                RestoreConnectors();
            }

            wasRestored = true;
        }

        private BaseLogicalGateControl ParseLogicGateFromRecord(DataBaseRecord record)
        {
            try
            {
                var simpleLogicalFactory = new SimpleLogicalGatesFactory();
                return simpleLogicalFactory.Create(record.Type);
            }
            catch (NotSupportedTypeOfArgumentException err)
            {
                throw new NotSupportedTypeOfArgumentException($"no matche's for {record.Type}");
            }
        }
        private void RestoreLogicGates() 
        {
            foreach(var logicGateRecord in DataBaseEntityModel.DataSet)
            {
                var control = ParseLogicGateFromRecord(logicGateRecord);
                control.Hash = logicGateRecord.HashCode;

                control.RenderTransform = new TranslateTransform(
                    logicGateRecord.CanvasX, logicGateRecord.CanvasY);

                control.manipulator.SetInitValues(logicGateRecord.CanvasX, logicGateRecord.CanvasY,
                    logicGateRecord.CanvasX,logicGateRecord.CanvasY);

                if (control is BaseOneChannelLogicalContorl oneChannelControl)
                {
                    if(control.ElementName != "Буфер" && control.ElementName != "BUF")// corner case's 
                    {
                        oneChannelControl.Input = Convert.ToBoolean(logicGateRecord.Input_1Value);
                    }             
                }

                if(control is BaseTwoChannelLogicalControl twoChannelControl)
                {
                    twoChannelControl.Input_1 = Convert.ToBoolean(logicGateRecord.Input_1Value);
                    twoChannelControl.Input_2 = Convert.ToBoolean(logicGateRecord.Input_2Value);
                }

                hashCodeToControlDict.Add(logicGateRecord.HashCode, control);
                appMainViewModel.SpawnOnCanvas(control, isRestoring: true);
            }
        }

        private void RestoreConnectors()
        {
            foreach (var logicGatesRecord in DataBaseEntityModel.DataSet)
            {
                var inputGate = hashCodeToControlDict[logicGatesRecord.HashCode];

                // ищем соответствующей элемент из котрого воткнули в 1 сокет inputGate
                if (logicGatesRecord.HashCodeInput_1 != 0)
                {
                    var outputGate = hashCodeToControlDict[logicGatesRecord.HashCodeInput_1];
                    var connector = new Connector(outputGate, inputGate, InputSockets.firstSocket);
   
                    outputGate.outputConenctor = connector;

                    if (inputGate is BaseOneChannelLogicalContorl oneChannelControl)
                    {
                        oneChannelControl.inputConnector = connector;
                    }

                    if(inputGate is BaseTwoChannelLogicalControl twoChannelControl)
                    {
                        twoChannelControl.input1Connector = connector;
                    }

                    appMainViewModel.SpawnConnector(connector);
                }

                // ищем соответствующей элемент из котрого воткнули во 2 сокет inputGate
                if (logicGatesRecord.HashCodeInput_2 != 0)
                {
                    var outputGate = hashCodeToControlDict[logicGatesRecord.HashCodeInput_2];
                    var connector = new Connector(outputGate, inputGate, InputSockets.secondSocket);

                    outputGate.outputConenctor = connector;

                    if(inputGate is BaseTwoChannelLogicalControl twoChannelControl)
                    {
                        twoChannelControl.input2Connector = connector;
                    }

                    appMainViewModel.SpawnConnector(connector);
                }
            }
        }
    }
}
