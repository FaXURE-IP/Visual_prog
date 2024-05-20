using Avalonia;
using Avalonia.Media;
using LogicGatesAvalonia.Models.LogicalGatesFactory;
using LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates;
using System;
using System.Globalization;


namespace LogicGatesAvalonia.Controls
{
    public class ANSILogicalBufferControl : BaseOneChannelLogicalContorl
    {
        public ANSILogicalBufferControl()
        {
            startPointX = 50;
            startPointY = 50;
            outputPosX = 3 * startPointX - elipseRadious;
            outputPosY = startPointY;
            elipseRadious = 12;
            penThickness = 3;

            var factroy = new ANSILogicalGatesFactory<LogicalBuffer>();
            var logicalBuffer = factroy.Create("ansi Logical buffer") as LogicalBuffer;

            logicalBuffer.SetInputValue(true);
            logicalBuffer.ProcessSignal();

            this.Input = Convert.ToBoolean(logicalBuffer.GetInputValue());
            this.Output = Convert.ToInt32(logicalBuffer.getOutputValue());
            this.ElementName = logicalBuffer.ElementName;

            this.model = logicalBuffer;
        }

        public override object Clone()
        {
            return new ANSILogicalBufferControl();
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
