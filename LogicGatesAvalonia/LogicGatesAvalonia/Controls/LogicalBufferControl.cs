using Avalonia.Media;
using System;
using System.Globalization;

using LogicGatesAvalonia.Models.LogicalGatesFactory;
using LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates;
using Avalonia;

namespace LogicGatesAvalonia.Controls
{
    public class LogicalBufferControl : BaseOneChannelLogicalContorl
    {
        public LogicalBufferControl()
        {
            startPointX = 50;
            startPointY = 50;
            elipseRadious = 12;
            outputPosX = 3 * startPointX - elipseRadious;
            outputPosY = startPointY;
            penThickness = 3;

            var factroy = new GOSTLogicalGatesFactory<LogicalBuffer>();
            var logicalBuffer = factroy.Create("gost Logical buffer") as LogicalBuffer;

            logicalBuffer.SetInputValue(true);
            logicalBuffer.ProcessSignal();

            this.Input = Convert.ToBoolean(logicalBuffer.GetInputValue());
            this.Output = Convert.ToInt32(logicalBuffer.getOutputValue());
            this.ElementName = logicalBuffer.ElementName;

            this.model = logicalBuffer;
        }

        public override object Clone()
        {
            return new LogicalBufferControl();
        }

        protected override void RenderDesiginationSymbol(DrawingContext context)
        {
            var formattedElementDesigination = new FormattedText(
                "1",
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
                new Avalonia.Point(startPointX + penThickness, startPointY * 2));
        }

        protected override void RenderInputChannel(DrawingContext context)
        {
        }

        protected override void RenderInputLine(DrawingContext context)
        {
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
