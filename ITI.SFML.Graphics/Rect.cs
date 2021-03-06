using System;
using System.Runtime.InteropServices;
using SFML.System;

namespace SFML.Graphics
{
    /// <summary>
    /// IntRect is an utility class for manipulating 2D rectangles
    /// with integer coordinates.
    /// </summary>
    [StructLayout( LayoutKind.Sequential )]
    public readonly struct IntRect : IEquatable<IntRect>
    {
        /// <summary>
        /// Left coordinate of the rectangle.
        /// </summary>
        public readonly int Left;

        /// <summary>
        /// Top coordinate of the rectangle.
        /// </summary>
        public readonly int Top;

        /// <summary>
        /// Width of the rectangle.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// Height of the rectangle.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Constructs the rectangle from its coordinates.
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle.</param>
        /// <param name="top">Top coordinate of the rectangle.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        public IntRect( int left, int top, int width, int height )
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Constructs the rectangle from position and size.
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle.</param>
        /// <param name="size">Size of the rectangle.</param>
        public IntRect( Vector2i position, Vector2i size )
            : this( position.X, position.Y, size.X, size.Y )
        {
        }

        /// <summary>
        /// Check if a point is inside the rectangle's area.
        /// </summary>
        /// <param name="x">X coordinate of the point to test</param>
        /// <param name="y">Y coordinate of the point to test</param>
        /// <returns>True if the point is inside</returns>
        public bool Contains( int x, int y )
        {
            int minX = Math.Min( Left, Left + Width );
            int maxX = Math.Max( Left, Left + Width );
            int minY = Math.Min( Top, Top + Height );
            int maxY = Math.Max( Top, Top + Height );

            return (x >= minX) && (x < maxX) && (y >= minY) && (y < maxY);
        }

        /// <summary>
        /// Gets whether this rectangle is the zero one.
        /// </summary>
        public bool IsZero => Top == 0 && Left == 0 && Width == 0 && Height == 0;

        /// <summary>
        /// Gets whether this rectangle intersects another one.
        /// </summary>
        /// <param name="rect">Other rectangle.</param>
        /// <returns>True if rectangles overlap.</returns>
        public bool IsIntersecting( IntRect rect ) => !Intersects( rect ).IsZero;

        /// <summary>
        /// Gets the intersection between this rectangle and another one.
        /// </summary>
        /// <param name="rect">Other rectangle.</param>
        /// <returns>The intersectio rectangles or a <see cref="IsZero"/> rectangle.</returns>
        public IntRect Intersects( IntRect rect )
        {
            // Rectangles with negative dimensions are allowed, so we must handle them correctly

            // Compute the min and max of the first rectangle on both axes
            int r1MinX = Math.Min( Left, Left + Width );
            int r1MaxX = Math.Max( Left, Left + Width );
            int r1MinY = Math.Min( Top, Top + Height );
            int r1MaxY = Math.Max( Top, Top + Height );

            // Compute the min and max of the second rectangle on both axes
            int r2MinX = Math.Min( rect.Left, rect.Left + rect.Width );
            int r2MaxX = Math.Max( rect.Left, rect.Left + rect.Width );
            int r2MinY = Math.Min( rect.Top, rect.Top + rect.Height );
            int r2MaxY = Math.Max( rect.Top, rect.Top + rect.Height );

            // Compute the intersection boundaries
            int interLeft = Math.Max( r1MinX, r2MinX );
            int interTop = Math.Max( r1MinY, r2MinY );
            int interRight = Math.Min( r1MaxX, r2MaxX );
            int interBottom = Math.Min( r1MaxY, r2MaxY );

            // If the intersection is valid (positive non zero area), then there is an intersection.
            int w = interRight - interLeft;
            int h = interBottom - interTop;
            return w > 0 && h > 0
                    ? new IntRect( interLeft, interTop, w, h )
                    : new IntRect();
        }

        /// <summary>
        /// Provides a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[IntRect]" +
                   " Left(" + Left + ")" +
                   " Top(" + Top + ")" +
                   " Width(" + Width + ")" +
                   " Height(" + Height + ")";
        }

        /// <summary>
        /// Checks if this rectangle is equal to an object.
        /// </summary>
        /// <param name="obj">Object to check.</param>
        /// <returns>True if the object is rectangle that is equal to this, false otherwise.</returns>
        public override bool Equals( object obj ) => obj is IntRect r && Equals( r );

        /// <summary>
        /// Compares two rectangles and checks if they are equal
        /// </summary>
        /// <param name="other">Rectangle to check</param>
        /// <returns>Rectangles are equal</returns>
        public bool Equals( IntRect other )
        {
            return (Left == other.Left) &&
                   (Top == other.Top) &&
                   (Width == other.Width) &&
                   (Height == other.Height);
        }

        /// <summary>
        /// Provide an integer describing the object.
        /// </summary>
        /// <returns>Integer description of the object.</returns>
        public override int GetHashCode()
        {
            return unchecked((int)((uint)Left ^
                   (((uint)Top << 13) | ((uint)Top >> 19)) ^
                   (((uint)Width << 26) | ((uint)Width >> 6)) ^
                   (((uint)Height << 7) | ((uint)Height >> 25))));
        }

        /// <summary>
        /// Operator == overload ; check rect equality.
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 == r2</returns>
        public static bool operator ==( IntRect r1, IntRect r2 ) => r1.Equals( r2 );

        /// <summary>
        /// Operator != overload ; check rect inequality.
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 != r2</returns>
        public static bool operator !=( IntRect r1, IntRect r2 ) => !r1.Equals( r2 );

        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        public static explicit operator FloatRect( IntRect r ) => new FloatRect( r.Left, r.Top, r.Width, r.Height );

    }

    /// <summary>
    /// FloatRect is an utility class for manipulating 2D rectangles
    /// with float coordinates.
    /// </summary>
    [StructLayout( LayoutKind.Sequential )]
    public readonly struct FloatRect : IEquatable<FloatRect>
    {
        /// <summary>
        /// Left coordinate of the rectangle.
        /// </summary>
        public readonly float Left;

        /// <summary>
        /// Top coordinate of the rectangle.
        /// </summary>
        public readonly float Top;

        /// <summary>
        /// Width of the rectangle.
        /// </summary>
        public readonly float Width;

        /// <summary>
        /// Height of the rectangle.
        /// </summary>
        public readonly float Height;

        /// <summary>
        /// Construct the rectangle from its coordinates.
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle.</param>
        /// <param name="top">Top coordinate of the rectangle.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        public FloatRect( float left, float top, float width, float height )
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Construct the rectangle from position and size.
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle.</param>
        /// <param name="size">Size of the rectangle.</param>
        public FloatRect( Vector2f position, Vector2f size )
            : this( position.X, position.Y, size.X, size.Y )
        {
        }

        /// <summary>
        /// Check if a point is inside the rectangle's area.
        /// </summary>
        /// <param name="x">X coordinate of the point to test.</param>
        /// <param name="y">Y coordinate of the point to test.</param>
        /// <returns>True if the point is inside.</returns>
        public bool Contains( float x, float y )
        {
            float minX = Math.Min( Left, Left + Width );
            float maxX = Math.Max( Left, Left + Width );
            float minY = Math.Min( Top, Top + Height );
            float maxY = Math.Max( Top, Top + Height );

            return (x >= minX) && (x < maxX) && (y >= minY) && (y < maxY);
        }

        /// <summary>
        /// Gets whether this rectangle is the (0,0,0,0) one.
        /// </summary>
        public bool IsZero => Top == 0 && Left == 0 && Width == 0 && Height == 0;

        /// <summary>
        /// Checks intersection between two rectangles.
        /// </summary>
        /// <param name="rect">Rectangle to test.</param>
        /// <returns>True if rectangles overlap.</returns>
        public bool IsIntersecting( FloatRect rect ) => !Intersects( rect ).IsZero;

        /// <summary>
        /// Returns the intersection between two rectangles.
        /// </summary>
        /// <param name="rect">Other rectangle.</param>
        /// <returns>Intersectd rectangle. </returns>
        public FloatRect Intersects( FloatRect rect )
        {
            // Rectangles with negative dimensions are allowed, so we must handle them correctly.

            // Compute the min and max of the first rectangle on both axes
            float r1MinX = Math.Min( Left, Left + Width );
            float r1MaxX = Math.Max( Left, Left + Width );
            float r1MinY = Math.Min( Top, Top + Height );
            float r1MaxY = Math.Max( Top, Top + Height );

            // Compute the min and max of the second rectangle on both axes
            float r2MinX = Math.Min( rect.Left, rect.Left + rect.Width );
            float r2MaxX = Math.Max( rect.Left, rect.Left + rect.Width );
            float r2MinY = Math.Min( rect.Top, rect.Top + rect.Height );
            float r2MaxY = Math.Max( rect.Top, rect.Top + rect.Height );

            // Compute the intersection boundaries
            float interLeft = Math.Max( r1MinX, r2MinX );
            float interTop = Math.Max( r1MinY, r2MinY );
            float interRight = Math.Min( r1MaxX, r2MaxX );
            float interBottom = Math.Min( r1MaxY, r2MaxY );

            // If the intersection is valid (positive non zero area), then there is an intersection.
            float w = interRight - interLeft;
            float h = interBottom - interTop;
            return w > 0 && h > 0
                    ? new FloatRect( interLeft, interTop, w, h )
                    : new FloatRect();
        }

        /// <summary>
        /// Provides a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[FloatRect]" +
                   " Left(" + Left + ")" +
                   " Top(" + Top + ")" +
                   " Width(" + Width + ")" +
                   " Height(" + Height + ")";
        }

        /// <summary>
        /// Compares rectangle and object and checks if they are equal.
        /// </summary>
        /// <param name="obj">Object to check.</param>
        /// <returns>True if this rectangle and the object are equal.</returns>
        public override bool Equals( object obj ) => obj is FloatRect r && Equals( r );

        /// <summary>
        /// Compares two rectangles and checks if they are equal.
        /// </summary>
        /// <param name="other">Rectangle to check.</param>
        /// <returns>Rectangles are equal.</returns>
        public bool Equals( FloatRect other )
        {
            return (Left == other.Left) &&
                   (Top == other.Top) &&
                   (Width == other.Width) &&
                   (Height == other.Height);
        }

        /// <summary>
        /// Provides a integer describing the object.
        /// </summary>
        /// <returns>Integer description of the object.</returns>
        public override int GetHashCode()
        {
            return unchecked((int)((uint)Left ^
                   (((uint)Top << 13) | ((uint)Top >> 19)) ^
                   (((uint)Width << 26) | ((uint)Width >> 6)) ^
                   (((uint)Height << 7) | ((uint)Height >> 25))));
        }

        /// <summary>
        /// Operator == overload ; check rect equality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 == r2</returns>
        public static bool operator ==( FloatRect r1, FloatRect r2 ) => r1.Equals( r2 );

        /// <summary>
        /// Operator != overload ; check rect inequality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 != r2</returns>
        public static bool operator !=( FloatRect r1, FloatRect r2 ) => !r1.Equals( r2 );

        /// <summary>
        /// Explicit casting to another rectangle type.
        /// </summary>
        /// <param name="r">Rectangle being casted.</param>
        /// <returns>Casting result.</returns>
        public static explicit operator IntRect( FloatRect r )
        {
            return new IntRect( (int)r.Left,
                                (int)r.Top,
                                (int)r.Width,
                                (int)r.Height );
        }

    }
}
