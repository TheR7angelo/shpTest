using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace shpTest.Test;

public static partial class Shp
{
    public static void Read(string filename, int srid)
    {
        var sequenceFactory = new NetTopologySuite.Geometries.Implementation.DotSpatialAffineCoordinateSequenceFactory(Ordinates.XY);

        var factory = new GeometryFactory(new PrecisionModel(PrecisionModels.Floating), srid, sequenceFactory);
        var reader = new ShapefileDataReader(filename, factory);

        var lst = reader.DbaseHeader.Fields.Select(descriptor => descriptor.Name).ToList();
        Console.WriteLine(string.Join(" | ", lst));

        while (reader.Read())
        {
            var lst2 = new List<string?>();
            for (var i = 1; i < reader.DbaseHeader.NumFields + 1; i++) lst2.Add(reader.GetValue(i).ToString());
            Console.WriteLine(string.Join(" | ", lst2));
        }
    }
}