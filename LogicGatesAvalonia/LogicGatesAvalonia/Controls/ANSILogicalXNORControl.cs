using Avalonia.Media;
using System;
using Avalonia;
using System.Globalization;

using LogicGatesAvalonia.Models.LogicalGatesFactory;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;

namespace LogicGatesAvalonia.Controls
{
    public class ANSILogicalXNORControl : BaseTwoChannelLogicalControl
    {
        public ANSILogicalXNORControl()
        {
            startPointX = 50;
            startPointY = 50;
            elipseRadious = 12;

            outputPosX = 4 * startPointX - elipseRadious;
            outputPosY = startPointY;

            input1PosX = elipseRadious;
            input1PosY = startPointX / 2;

            input2PosX = elipseRadious;
            input2PosY = startPointX + startPointX / 2;

            penThickness = 3;


            var factroy = new ANSILogicalGatesFactory<LogicalXNOR>();
            var logicalXNOR = factroy.Create("ansi Logical xnor") as LogicalXNOR;

            logicalXNOR.SetInputValue(false, false);
            logicalXNOR.ProcessSignal();

            this.Input_1 = Convert.ToBoolean(logicalXNOR.GetFirstInputValue());
            this.Input_2 = Convert.ToBoolean(logicalXNOR.GetSecondInputValue());

            this.Output = Convert.ToInt32(logicalXNOR.getOutputValue());
            this.ElementName = logicalXNOR.ElementName;

            this.model = logicalXNOR;
        }

        public override object Clone()
        {
            return new ANSILogicalXNORControl();
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
                new Avalonia.Point(startPointX + 18, startPointY * 2));
        }

        protected override void RenderInputChannel_1(DrawingContext context)
        {
            context.DrawEllipse( // drawing input channel
                Input_1ChannelActiveColor,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(input1PosX,input1PosY),
                elipseRadious,
                elipseRadious);
        }

        protected override void RenderInputChannel_2(DrawingContext context)
        {
            context.DrawEllipse( // drawing input channel
                Input_2ChannelActiveColor,
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(input2PosX,input2PosY),
                elipseRadious,
                elipseRadious);
        }

        protected override void RenderInputLine_1(DrawingContext context)
        {
            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * elipseRadious, startPointY / 2),
                new Avalonia.Point(startPointX + 5, startPointY / 2));
        }

        protected override void RenderInputLine_2(DrawingContext context)
        {
            context.DrawLine(
                new Pen(Brushes.Black, penThickness),
                new Avalonia.Point(2 * elipseRadious, startPointY + startPointY / 2),
                new Avalonia.Point(startPointX + 5, startPointY + startPointY / 2));
        }

        protected override void RenderMainBody(DrawingContext context)
        {
            Pen pen = new Pen(Brushes.Black, penThickness);
            StreamGeometry geometry = new StreamGeometry();

            using (StreamGeometryContext geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(new Avalonia.Point(startPointX, 0), false);
                geometryContext.ArcTo(new Avalonia.Point(startPointX, startPointY * 2), new Size(50, 65),
                    0,
                    false,
                    SweepDirection.Clockwise);
            }

            using (StreamGeometryContext geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(new Avalonia.Point(startPointX - 10, 0), false);
                geometryContext.ArcTo(new Avalonia.Point(startPointX - 10, startPointY * 2), new Size(50, 65),
                    0,
                    false,
                    SweepDirection.Clockwise);
            }

            context.DrawLine(
                pen,
                new Avalonia.Point(startPointX, 0),
                new Avalonia.Point(startPointX * 2, 0)
                );

            context.DrawLine(
                pen,
                new Avalonia.Point(startPointX, startPointY * 2),
                new Avalonia.Point(startPointX * 2, startPointY * 2)
                );

            using (StreamGeometryContext geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(new Avalonia.Point(startPointX * 2, 0), false);
                geometryContext.ArcTo(new Avalonia.Point(startPointX * 3, startPointY), new Size(50, 50),
                    0,
                    false,
                    SweepDirection.Clockwise);
            }

            using (StreamGeometryContext geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(new Avalonia.Point(startPointX * 2, startPointY * 2), false);
                geometryContext.ArcTo(new Avalonia.Point(startPointX * 3, startPointY), new Size(50, 50),
                    0,
                    false,
                    SweepDirection.CounterClockwise);
            }

            context.DrawGeometry(Brushes.Transparent, pen, geometry);
        }

        protected override void RenderOutput(DrawingContext context)
        {
            Pen pen = new Avalonia.Media.Pen(Brushes.Black, penThickness);

            context.DrawLine(
               pen,
               new Avalonia.Point(3 * startPointX, startPointY),
               new Avalonia.Point(4 * startPointX - elipseRadious, startPointY)
              );

            context.DrawEllipse( // drawing output channel
                Brushes.White,
                pen,
                new Avalonia.Point(outputPosX, outputPosY),
                elipseRadious,
                elipseRadious);

            context.DrawEllipse( // drawing invertor circle 
            Brushes.Black,
                pen,
                new Avalonia.Point(3 * startPointX, startPointY),
                elipseRadious / 2,
                elipseRadious / 2);

            var formattedOutput = new FormattedText(Convert.ToInt32(Output).ToString(),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                Typeface.Default,
                TextSize,
                Brushes.Black);

            context.DrawText(formattedOutput,
                new Avalonia.Point(startPointX * 4 - elipseRadious - 3, startPointY - elipseRadious));
        }
    }
}
