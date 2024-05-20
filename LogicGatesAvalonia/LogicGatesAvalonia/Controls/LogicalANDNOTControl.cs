using Avalonia.Media;
using System;
using System.Globalization;

using LogicGatesAvalonia.Models.LogicalGatesFactory;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;


namespace LogicGatesAvalonia.Controls
{
    public class LogicalANDNOTControl : BaseTwoChannelLogicalControl
    {
        public LogicalANDNOTControl()
        {
            startPointX = 50;
            startPointY = 50;
            elipseRadious = 12;

            input1PosX = elipseRadious;
            input1PosY = startPointX / 2;

            input2PosX = elipseRadious;
            input2PosY = startPointX + startPointX / 2;

            outputPosX = 3 * startPointX - elipseRadious;
            outputPosY = startPointY;


            penThickness = 3;

            var factroy = new GOSTLogicalGatesFactory<LogicalANDNOT>();
            var logicalANDNOT = factroy.Create("gost Logical and not") as LogicalANDNOT;

            logicalANDNOT.SetInputValue(false, false);
            logicalANDNOT.ProcessSignal();

            this.Input_1 = Convert.ToBoolean(logicalANDNOT.GetFirstInputValue());
            this.Input_2 = Convert.ToBoolean(logicalANDNOT.GetSecondInputValue());

            this.Output = Convert.ToInt32(logicalANDNOT.getOutputValue());
            this.ElementName = logicalANDNOT.ElementName;

            this.model = logicalANDNOT;
        }

        public override object Clone()
        {
            return new LogicalANDNOTControl();
        }

        protected override void RenderDesiginationSymbol(DrawingContext context)
        {
            var formattedElementDesigination = new FormattedText(
                "&",
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                Typeface.Default,
                TextSize,
                SelectedTextColor);

            context.DrawText(formattedElementDesigination,
                new Avalonia.Point(startPointX + 18, startPointY - 15));
        }

        protected override void RenderElementName(DrawingContext context)
        {
            var formattedElementName = new FormattedText(
                model.ElementName,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                Typeface.Default,
                TextSize,
                SelectedTextColor);

            context.DrawText(formattedElementName,
                new Avalonia.Point(startPointX + 8, startPointY * 2));
        }

        protected override void RenderInputChannel_1(DrawingContext context)
        {
            context.DrawEllipse( // drawing input channel
                Input_1ChannelActiveColor,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(input1PosX, input1PosY),
                elipseRadious,
                elipseRadious);
        }

        protected override void RenderInputChannel_2(DrawingContext context)
        {
            context.DrawEllipse( // drawing input channel
                Input_2ChannelActiveColor,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(input2PosX, input2PosY),
                elipseRadious,
                elipseRadious);
        }

        protected override void RenderInputLine_1(DrawingContext context)
        {
            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * elipseRadious, startPointY / 2),
                new Avalonia.Point(startPointX, startPointY / 2));
        }

        protected override void RenderInputLine_2(DrawingContext context)
        {
            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * elipseRadious, startPointY + startPointY / 2),
                new Avalonia.Point(startPointX, startPointY + startPointY / 2));
        }

        protected override void RenderMainBody(DrawingContext context)
        {
            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(startPointX, startPointY),
                new Avalonia.Point(startPointX, 2 * startPointY)
               );

            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(startPointX, 2 * startPointY),
                new Avalonia.Point(2 * startPointX, 2 * startPointY)
               );

            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * startPointX, 2 * startPointY),
                new Avalonia.Point(2 * startPointX, startPointY)
               );

            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * startPointX, startPointY),
                new Avalonia.Point(2 * startPointX, 0));

            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * startPointX, 0),
                new Avalonia.Point(startPointX, 0));

            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(startPointX, 0),
                new Avalonia.Point(startPointX, startPointY));
        }

        protected override void RenderOutput(DrawingContext context)
        {
            context.DrawLine(
               new Pen(Brushes.Black, penThickness),
               new Avalonia.Point(2 * startPointX, startPointY),
               new Avalonia.Point(3 * startPointX - elipseRadious, startPointY)
              );

            context.DrawEllipse( // drawing output channel
                Brushes.White,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(outputPosX, outputPosY),
                elipseRadious,
                elipseRadious);

            context.DrawEllipse( // drawing invertor circle 
                Brushes.Black,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * startPointX, startPointY),
                elipseRadious / 2,
                elipseRadious / 2);

            var formattedOutput = new FormattedText(Convert.ToInt32(Output).ToString(),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                Typeface.Default,
                TextSize,
                Brushes.Black);

            context.DrawText(formattedOutput,
                new Avalonia.Point(startPointX * 3 - elipseRadious - 3, startPointY - elipseRadious));
        }
    }
}
