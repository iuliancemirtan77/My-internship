using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsOverloading
{
    struct Angle
    {
        public int Degrees { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

    
        public Angle(int degr, int min, int sec)
        {
            Degrees = degr;
            Minutes = min;
            Seconds = sec;
        }
        public Angle GetNormalizedAngle()
        {
            return normalize(new Angle(Degrees, Minutes, Seconds));
        }
        public Angle NormalizeAngle()
        {
            normalize(this);
            return this;
        }
        private static Angle normalize(Angle angle)
        {
            // Seconds' Normalization
            int excees = angle.Seconds / 60;
            angle.Seconds %= 60;
            angle.Minutes += excees;

            // Minutes' Normalization
            excees = angle.Minutes / 60;
            angle.Minutes %= 60;
            angle.Degrees += excees;

            // Degrees' Normalization
            angle.Degrees %= 360;

            return angle;
        }
        public Angle GetOptimizedAngle()
        {
            return optimizedAngle(new Angle(Degrees, Minutes, Seconds));
        }
        public Angle OptimizeAngle()
        {
            optimizedAngle(this);
            return this;
        }
        private static Angle optimizedAngle(Angle angle)
        {
            // Normalization
            angle = normalize(angle);

            // Adjust that three componets to a standart representation 
            if (angle.Degrees < 0)
            {
                if (angle.Minutes > 0)
                {
                    angle.Minutes = 60 - angle.Minutes;
                }
                if (angle.Seconds > 0)
                {
                    angle.Seconds = 60 - angle.Seconds;
                }
            }
            else if (angle.Degrees > 0)
            {
                if (angle.Minutes < 0)
                {
                    angle.Minutes += 60;
                }
                if (angle.Seconds < 0)
                {
                    angle.Seconds += 60;
                }
            }

            int angleAsSeconds = angle.Degrees * 3600 + angle.Minutes * 60 + angle.Seconds;

            if (angleAsSeconds < 0)
            {
                // Angle is NEGATIVE (0..360)

                // Negative Angle in Lower semicircle (0..-180)
                //if (angle.Degrees < 0 && angle.Degrees > -180)
                if (angleAsSeconds < 0 && angleAsSeconds > -648000)
                {
                    // Transform to negative all three components (dimensions)
                    if (angle.Minutes > 0)
                    {
                        angle.Minutes = 60 - angle.Minutes;
                    }
                    if (angle.Seconds > 0)
                    {
                        angle.Seconds = 60 - angle.Seconds;
                    }
                }

                // Negative Angle in Upper semicircle [-180..360)
                else
                {
                    // Transform to positive all three components (dimensions)
                    if (angle.Minutes != 0 || angle.Seconds != 0)
                    {
                        angle.Degrees += 360 - 1;
                    }
                    else
                    {
                        angle.Degrees += 360;
                    }

                    if (angle.Minutes < 0)
                    {
                        angle.Minutes += 60;
                    }
                    if (angle.Seconds < 0)
                    {
                        angle.Seconds += 60;
                    }
                }
            }

            // Angle is POSITIVE
            else
            {
                // Positive Angle in Upper semicircle [0..180]
                //if (angle.Degrees >= 0 && angle.Degrees <= 180)
                if (angleAsSeconds >= 0 && angleAsSeconds <= 648000)
                {
                    // Transform to positive all three components
                    if (angle.Minutes < 0)
                    {
                        angle.Minutes += 60;
                    }
                    if (angle.Seconds < 0)
                    {
                        angle.Seconds += 60;
                    }
                }

                // Positive Angle in Lower semicircle (180..0)
                else
                {
                    // Transform to positive all three components
                    if(angle.Minutes != 0 || angle.Seconds != 0)
                    {
                        angle.Degrees += -360 + 1;
                    }
                    else
                    {
                        angle.Degrees += -360;
                    }

                    if (angle.Minutes > 0)
                    {
                        angle.Minutes += -60;
                    }
                    if (angle.Seconds > 0)
                    {
                        angle.Seconds += -60;
                    }
                }
            }

            return angle;
        }
        public static Angle operator +(Angle lhs, Angle rhs)
        {
            Angle result = new Angle();

            result.Degrees = lhs.Degrees + rhs.Degrees;
            result.Minutes = lhs.Minutes + rhs.Minutes;
            result.Seconds = lhs.Seconds + rhs.Seconds;

            // Optimization:
            // - Negative degrees should belong to the LOWER Semicircle
            // - Positive degrees should belong to the UPPER Semicircle
            return optimizedAngle(result);
        }
        public static Angle operator -(Angle lhs, Angle rhs)
        {
            Angle result = new Angle();
            
            result.Degrees = lhs.Degrees - rhs.Degrees;
            result.Minutes = lhs.Minutes - rhs.Minutes;
            result.Seconds = lhs.Seconds - rhs.Seconds;

            // Optimization:
            // - Negative degrees should belong to the LOWER Semicircle
            // - Positive degrees should belong to the UPPER Semicircle
            return optimizedAngle(result);
        }
        public static Angle operator *(Angle lhs, int scalar)
        {
            Angle result = new Angle(lhs.Degrees, lhs.Minutes, lhs.Seconds);

            int resultExpressedInSeconds = Math.Abs(result.Degrees * 3600 + result.Minutes * 60 + result.Seconds) * scalar;

            result.Degrees = resultExpressedInSeconds / 3600;
            result.Minutes = (resultExpressedInSeconds - result.Degrees * 3600) / 60;
            result.Seconds = resultExpressedInSeconds - result.Degrees * 3600 - result.Minutes * 60;
            
            // Optimization:
            // - Negative degrees should belong to the LOWER Semicircle
            // - Positive degrees should belong to the UPPER Semicircle
            return optimizedAngle(result);
        }
        public static Angle operator *(int scalar, Angle rhs)
        {
            Angle result = new Angle(rhs.Degrees, rhs.Minutes, rhs.Seconds);

            int resultExpressedInSeconds = Math.Abs(result.Degrees * 3600 + result.Minutes * 60 + result.Seconds) * scalar;

            result.Degrees = resultExpressedInSeconds / 3600;
            result.Minutes = (resultExpressedInSeconds - result.Degrees * 3600) / 60;
            result.Seconds = resultExpressedInSeconds - result.Degrees * 3600 - result.Minutes * 60;

            // Optimization:
            // - Negative degrees should belong to the LOWER Semicircle
            // - Positive degrees should belong to the UPPER Semicircle
            return optimizedAngle(result);
        }
        public static Angle operator /(Angle lhs, int scalar)
        {
            Angle result = new Angle(lhs.Degrees, lhs.Minutes, lhs.Seconds);

            int resultExpressedInSeconds = Math.Abs(result.Degrees * 3600 + result.Minutes * 60 + result.Seconds) / scalar;

            result.Degrees = resultExpressedInSeconds / 3600;
            result.Minutes = (resultExpressedInSeconds - result.Degrees * 3600) / 60;
            result.Seconds = resultExpressedInSeconds - result.Degrees * 3600 - result.Minutes * 60;

            // Optimization:
            // - Negative degrees should belong to the LOWER Semicircle
            // - Positive degrees should belong to the UPPER Semicircle
            return optimizedAngle(result);
        }
       
        public static bool operator >(Angle lhs, Angle rhs)
        {
            Angle a = new Angle(lhs.Degrees, lhs.Minutes, lhs.Seconds);
            Angle b = new Angle(rhs.Degrees, rhs.Minutes, rhs.Seconds);

            a.OptimizeAngle();
            b.OptimizeAngle();

            int aExpressedInSeconds = Math.Abs(a.Degrees * 3600 + a.Minutes * 60 + a.Seconds);
            int bExpressedInSeconds = Math.Abs(b.Degrees * 3600 + b.Minutes * 60 + b.Seconds);

            return (aExpressedInSeconds > bExpressedInSeconds);
        }
        public static bool operator <(Angle lhs, Angle rhs)
        {
            Angle a = new Angle(lhs.Degrees, lhs.Minutes, lhs.Seconds);
            Angle b = new Angle(rhs.Degrees, rhs.Minutes, rhs.Seconds);

            a.OptimizeAngle();
            b.OptimizeAngle();

            int aExpressedInSeconds = Math.Abs(a.Degrees * 3600 + a.Minutes * 60 + a.Seconds);
            int bExpressedInSeconds = Math.Abs(b.Degrees * 3600 + b.Minutes * 60 + b.Seconds);

            return (aExpressedInSeconds < bExpressedInSeconds);
        }

        public override string ToString()
        {
            return string.Format(
                            " Degrees: {0,4}  >>>  Minutes:{1,4}  >>>  Seconds:{2,4}",
                             Degrees, Minutes, Seconds
                          );
        }
    }
}
