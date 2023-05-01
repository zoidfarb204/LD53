using Enums;

namespace Models
{
    public class RoadProperties
    {
        public int Speed { get; set; }
        public int ChanceOfIntersection { get; set; }

        public RoadProperties GetPropertiesByType(RoadType roadType)
        {
            switch (roadType)
            {
                case RoadType.Highway:
                    return new RoadProperties
                    {
                        Speed = 60,
                        ChanceOfIntersection = 10
                    };
                case RoadType.Interstate:
                    return new RoadProperties
                    {
                        Speed = 60,
                        ChanceOfIntersection = 5
                    };
                case RoadType.Commercial:
                    return new RoadProperties
                    {
                        Speed = 40,
                        ChanceOfIntersection = 20
                    };
                case RoadType.Residential:
                    return new RoadProperties
                    {
                        Speed = 25,
                        ChanceOfIntersection = 50
                    };
                case RoadType.Major:
                    return new RoadProperties
                    {
                        Speed = 45,
                        ChanceOfIntersection = 25
                    };
                default:
                    return new RoadProperties
                    {
                        Speed = 25,
                        ChanceOfIntersection = 20
                    };
            }
        } 
    }
}