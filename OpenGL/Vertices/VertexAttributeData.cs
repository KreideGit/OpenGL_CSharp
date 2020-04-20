namespace OpenGL.Vertices
{
    public enum RepresentingType
    {
        Position,
        TextureCoordinate,
        Normal,
        Other
    }

    public class VertexAttributeData
    {
        public string Name { get; }
        public RepresentingType RepresentingType { get; }
        public AttributeType Type { get; }
        public VertexAttribType DataType { get; }
        public int DataLength { get; }

        public VertexAttributeData(string name, RepresentingType representingType, AttributeType type, VertexAttribType dataType)
        {
            Name = name;
            RepresentingType = representingType;
            Type = type;
            DataType = dataType;

            switch (Type)
            {
                case AttributeType.FloatVec2: DataLength = 2; break;
                case AttributeType.FloatVec3: DataLength = 3; break;
                case AttributeType.FloatVec4: DataLength = 4; break;
            }
        }
    }
}