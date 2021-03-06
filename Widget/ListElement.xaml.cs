using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinkedListVisualization.Widget
{
    /// <summary>
    /// ListElement.xaml 的交互逻辑
    /// </summary>
    public partial class ListElement : Viewbox
    {
        // 非线性动画Easing function
        private static NonLinearEasingFunction nonLinearEasingFunction = new NonLinearEasingFunction(16)
        {
            EasingMode = EasingMode.EaseIn
        };

        public ListElement()
        {
            InitializeComponent();
            Ring.Opacity = 0;
            Content.Opacity = 0;
        }

        public double currentCanvasLeft = 0;
        public double currentCanvasTop = 0;

        public ListElement(int number, byte r, byte g, byte b)
        {
            InitializeComponent();
            Ring.Opacity = 0;
            Content.Opacity = 0;
            SetProperty(number, r, g, b);
        }

        public double Show(Storyboard storyboard, double prevCompleteTime)
        {
            DoubleAnimation ringDoubleAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(700)))
            {
                BeginTime = TimeSpan.FromSeconds(prevCompleteTime)
            };
            DoubleAnimation contentDoubleAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(700)))
            {
                BeginTime = TimeSpan.FromSeconds(prevCompleteTime)
            };

            ringDoubleAnimation.EasingFunction = nonLinearEasingFunction;
            contentDoubleAnimation.EasingFunction = nonLinearEasingFunction;

            Storyboard.SetTarget(ringDoubleAnimation, Ring);
            Storyboard.SetTargetProperty(ringDoubleAnimation, new PropertyPath("Opacity"));

            Storyboard.SetTarget(contentDoubleAnimation, Content);
            Storyboard.SetTargetProperty(contentDoubleAnimation, new PropertyPath("Opacity"));

            storyboard.Children.Add(ringDoubleAnimation);
            storyboard.Children.Add(contentDoubleAnimation);
            return prevCompleteTime + 0.7;
        }

        public double Close(Storyboard storyboard, double prevCompleteTime)
        {
            DoubleAnimation ringDoubleAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(700)))
            {
                BeginTime = TimeSpan.FromSeconds(prevCompleteTime)
            };
            DoubleAnimation contentDoubleAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(700)))
            {
                BeginTime = TimeSpan.FromSeconds(prevCompleteTime)
            };

            ringDoubleAnimation.EasingFunction = nonLinearEasingFunction;
            contentDoubleAnimation.EasingFunction = nonLinearEasingFunction;

            Storyboard.SetTarget(ringDoubleAnimation, Ring);
            Storyboard.SetTargetProperty(ringDoubleAnimation, new PropertyPath("Opacity"));

            Storyboard.SetTarget(contentDoubleAnimation, Content);
            Storyboard.SetTargetProperty(contentDoubleAnimation, new PropertyPath("Opacity"));

            storyboard.Children.Add(ringDoubleAnimation);
            storyboard.Children.Add(contentDoubleAnimation);
            return prevCompleteTime + 0.7;
        }

        public void SetProperty(int number, byte r, byte g, byte b)
        {
            if (number >= 0)
            {
                Content.Content = number.ToString();
            }
            else
            {
                Content.Content = "H";
            }
            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
            Content.Foreground = solidColorBrush;
            Ring.Fill = solidColorBrush;
        }

        public double SetPropertyAnim(Storyboard storyboard, double prevCompleteTime, int number)
        {
            string targetContent;
            if (number >= 0)
            {
                targetContent = number.ToString();
            }
            else
            {
                targetContent = "H";
            }

            StringAnimationUsingKeyFrames changeAnim = new StringAnimationUsingKeyFrames();
            DiscreteStringKeyFrame changeBeforeFrame = new DiscreteStringKeyFrame((string)Content.Content, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            DiscreteStringKeyFrame changeAfterFrame = new DiscreteStringKeyFrame(targetContent, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4)));
            changeAnim.BeginTime = TimeSpan.FromSeconds(prevCompleteTime);
            changeAnim.KeyFrames.Add(changeBeforeFrame);
            changeAnim.KeyFrames.Add(changeAfterFrame);
            Storyboard.SetTarget(changeAnim, Content);
            Storyboard.SetTargetProperty(changeAnim, new PropertyPath("Content"));

            storyboard.Children.Add(changeAnim);

            /*
            DoubleAnimation hideAnim = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.4)))
            {
                EasingFunction = new ExponentialEase()
                {
                    EasingMode = EasingMode.EaseOut
                },
                BeginTime = TimeSpan.FromSeconds(prevCompleteTime)
            };
            Storyboard.SetTarget(hideAnim, Content);
            Storyboard.SetTargetProperty(hideAnim, new PropertyPath("Opacity"));
            storyboard.Children.Add(hideAnim);

            DoubleAnimation showAnim = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.4)))
            {
                EasingFunction = new ExponentialEase()
                {
                    EasingMode = EasingMode.EaseOut
                },
                BeginTime = TimeSpan.FromSeconds(prevCompleteTime + 0.4)
            };
            Storyboard.SetTarget(showAnim, Content);
            Storyboard.SetTargetProperty(showAnim, new PropertyPath("Opacity"));
            storyboard.Children.Add(showAnim);
            */
            return prevCompleteTime + 0.4;
        }

        public double Move(Storyboard storyboard, double prevCompleteTime, double deltaX, double deltaY)
        {
            DoubleAnimation xDoubleAnimation = new DoubleAnimation(currentCanvasLeft, currentCanvasLeft + deltaX, new Duration(TimeSpan.FromMilliseconds(1500)));
            xDoubleAnimation.BeginTime = TimeSpan.FromSeconds(prevCompleteTime);

            DoubleAnimation yDoubleAnimation = new DoubleAnimation(currentCanvasTop, currentCanvasTop + deltaY, new Duration(TimeSpan.FromMilliseconds(1500)));
            yDoubleAnimation.BeginTime = TimeSpan.FromSeconds(prevCompleteTime);


            xDoubleAnimation.EasingFunction = nonLinearEasingFunction;
            yDoubleAnimation.EasingFunction = nonLinearEasingFunction;

            Storyboard.SetTarget(xDoubleAnimation, this);
            Storyboard.SetTargetProperty(xDoubleAnimation, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTarget(yDoubleAnimation, this);
            Storyboard.SetTargetProperty(yDoubleAnimation, new PropertyPath("(Canvas.Top)"));

            storyboard.Children.Add(xDoubleAnimation);
            storyboard.Children.Add(yDoubleAnimation);

            currentCanvasLeft += deltaX;
            currentCanvasTop += deltaY;
            return prevCompleteTime + 1.5;
        }
    }
}
