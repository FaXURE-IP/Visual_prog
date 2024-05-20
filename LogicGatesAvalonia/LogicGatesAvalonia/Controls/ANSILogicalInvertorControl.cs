using Avalonia.Media;
using System;
using System.Globalization;

using LogicGatesAvalonia.Models.LogicalGatesFactory;
using LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates;


namespace LogicGatesAvalonia.Controls
{
    public class ANSILogicalInvertorControl : BaseOneChannelLogicalContorl
    {
        public ANSILogicalInvertorControl()
        {
            startPointX = 50;
            startPointY = 50;
            inputPosX = elipseRadious + 10;
            inputPosY = startPointY;
            outputPosX = 3 * startPointX - elipseRadious;
            outputPosY = startPointY;
            elipseRadious = 12;
            penThickness = 3;

            var factroy = new ANSILogicalGatesFactory<LogicalInvertor>();
            var logicalInvertor = factroy.Create("ansi Logical invertor") as LogicalInvertor;

            logicalInvertor.SetInputValue(false);
            logicalInvertor.ProcessSignal();

            this.model = logicalInvertor;

            this.Input = Convert.ToBoolean(logicalInvertor.GetInputValue());
            this.Output = Convert.ToInt32(logicalInvertor.getOutputValue());
            this.ElementName = logicalInvertor.ElementName;
        }

        public override object Clone()
        {
            return new ANSILogicalInvertorControl();
        }
        protected override void RenderDesiginationSymbol(DrawingContext renderWindow)
        {

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
                new Avalonia.Point(startPointX + penThickness, startPointY * 2));
        }

        protected override void RenderInputChannel(DrawingContext context)
        {
            context.DrawEllipse( // drawing input channel
                InputChannelActiveColor,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(inputPosX, inputPosY),
                elipseRadious,
                elipseRadious);
        }

        protected override void RenderInputLine(DrawingContext context)
        {
            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(startPointX, startPointY),
                new Avalonia.Point(startPointX, 0));
        }

        protected override void RenderMainBody(DrawingContext context)
        {
            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * elipseRadious, startPointY),
                new Avalonia.Point(startPointX, startPointY));

            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(startPointX, startPointY),
                new Avalonia.Point(startPointX, 2 * startPointY)
               );

            context.DrawLine(
               new Pen(Brushes.Black, penThickness),
               new Avalonia.Point(startPointX, 0),
               new Avalonia.Point(startPointX * 2, startPointY)
              );

            context.DrawLine(
               new Pen(Brushes.Black, penThickness),
               new Avalonia.Point(startPointX, startPointY * 2),
               new Avalonia.Point(startPointX * 2, startPointY)
              );
        }

        protected override void RenderOutput(DrawingContext context)
        {
            context.DrawLine(
              new Pen(Brushes.Black, penThickness),
              new Avalonia.Point(startPointX * 2, startPointY),
              new Avalonia.Point(startPointX * 3, startPointY)
             );

            context.DrawEllipse( // drawing invertor circle 
                Brushes.Black,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * startPointX, startPointY),
                elipseRadious / 2,
                elipseRadious / 2);


            context.DrawEllipse( // drawing output channel
                Brushes.White,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(outputPosX, outputPosY),
                elipseRadious,
                elipseRadious);

            var formattedOutput = new FormattedText(Convert.ToInt32(Output).ToString(),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                Typeface.Default,
                TextSize,
                Brushes.Black);

            context.DrawText(formattedOutput,
                new Avalonia.Point(startPointX * 3 - elipseRadious + 3 * penThickness, startPointY - elipseRadious));
        }
    }
}
