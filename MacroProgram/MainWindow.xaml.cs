using System;
using System.Windows;
using Timer = System.Timers.Timer;

namespace MacroProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Timer timer = new Timer(100);
            timer.Start();

            timer.Elapsed += (sender, args) =>
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    MouseOperations.MousePoint point = MouseOperations.GetCursorPosition();
                    Location.Content = string.Format("{0}, {1}", point.X, point.Y);
                }));

            };
        }

        private void OnStartButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var fX = int.Parse(firstX.Text);
                var fY = int.Parse(firstY.Text);
                var sX = int.Parse(secondX.Text);
                var sY = int.Parse(secondy.Text);
                var tX = int.Parse(thirdX.Text);
                var tY = int.Parse(thirdY.Text);

                var delay = int.Parse(this.delay.Text);

                if (delay == 0)
                {
                    MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(fX, fY));
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

                    MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(sX, sY));
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

                Dispatcher.Invoke(new Action(() =>
                {
                    if (Enable.IsChecked.Value)
                    {
                        MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(tX, tY));
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                    }
                }));
            } else if (delay > 0)
                {
                    Timer timer = new Timer(delay);
                    timer.Elapsed += (o, args) =>
                    {
                        timer.Stop();
                        MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(sX, sY));
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                        Dispatcher.Invoke(new Action(() =>
                        {
                            if (Enable.IsChecked.Value)
                        {
                            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(tX, tY));
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                        }
                        }));
                    };
                    timer.Start();
                    MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(fX, fY));
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                }
                else
                {
                    Timer timer = new Timer(-delay);
                    timer.Elapsed += (o, args) =>
                    {
                        timer.Stop();
                        MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(fX, fY));
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                    };
                    timer.Start();
                    MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(sX, sY));
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

                    if (Enable.IsChecked.Value)
                    {
                        MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(tX, tY));
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                    }
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message);
            }
        }
    }
}
