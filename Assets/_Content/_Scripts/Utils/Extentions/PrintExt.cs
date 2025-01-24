using UnityEngine;

public class PrintExt
{
    public static string VectorPrecise(Vector3 vector)
    {
        return $"({vector.x}, {vector.y}, {vector.z})";
    }

    public static string VectorPrecise(Vector4 vector)
	{
		return $"({vector.x}, {vector.y}, {vector.z}, {vector.w})";
    }

    public static string Matrix4x4(Matrix4x4 matrix)
	{
		return $"(Position: {matrix.GetPosition()}, Rotation (EULR): {matrix.rotation.eulerAngles}, Scale: {matrix.lossyScale})";
    }
}
