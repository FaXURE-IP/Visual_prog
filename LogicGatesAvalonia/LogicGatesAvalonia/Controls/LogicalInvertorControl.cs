using Avalonia.Media;
using System;
using System.Globalization;

using LogicGatesAvalonia.Models.LogicalGatesFactory;
using LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates;

namespace LogicGatesAvalonia.Controls
{
    public class LogicalInvertorControl : BaseOneChannelLogicalContorl
    {
        public LogicalInvertorControl()
        {
            startPointX = 50;
            startPointY = 50;
            elipseRadious = 12;
            outputPosX = 3 * startPointX - elipseRadious;
            outputPosY = startPointY;
            inputPosX = elipseRadious;
            inputPosY = startPointY;
            penThickness = 3;

            var factroy = new GOSTLogicalGatesFactory<LogicalInvertor>();
            var logicalInvertor = factroy.Create("gost Logical invertor") as LogicalInvertor;

            logicalInvertor.SetInputValue(false);
            logicalInvertor.ProcessSignal();

            this.model = logicalInvertor;

            this.Input = Convert.ToBoolean(logicalInvertor.GetInputValue());
            this.Output = Convert.ToInt32(logicalInvertor.getOutputValue());
            this.ElementName = logicalInvertor.ElementName;
        }

        public override object Clone()
        {
            return new LogicalInvertorControl();
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
                new Avalonia.Point(startPointX - 6, startPointY * 2));
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
                new Avalonia.Point(2 * elipseRadious, startPointY),
                new Avalonia.Point(startPointX, startPointY));
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
