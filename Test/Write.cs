using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace shpTest.Test;

public static partial class Shp
{
    public static void Write(string filename, int srid, Point point = new())
    {
        // const string filename = "test";
        // const int srid = 4326;

        var sequenceFactory = new NetTopologySuite.Geometries.Implementation.DotSpatialAffineCoordinateSequenceFactory(Ordinates.XY);
        var geomFactory = new GeometryFactory(new PrecisionModel(PrecisionModels.Floating), srid, sequenceFactory);

// use shapefilewriter to force geometryType a u want ex=point
        // var tmpFile = Path.GetTempFileName();
        //var shpWriter = new ShapefileWriter(geomFactory, tmpFile, ShapeGeometryType.Point);

// use the dataWriter to write the shape file with geometry and data
        var dataWriter = new ShapefileDataWriter(filename, geomFactory);                 

// create an entity and add to the features
        var features = new List<Feature>();
        var att = new AttributesTable { { "ID", 1 }, {"test", "yolo"} };


        var geomZ = geomFactory.CreatePoint(new Coordinate(point.X,point.Y));

// force geomz to geom xy using the shpWriter
        //var geom = shpWriter.Factory.CreateGeometry(geomZ);

// add to the features
        features.Add(new Feature(geomZ, att));
        features.Add(new Feature(geomZ, att));

// header
        var outDbaseHeader = new DbaseFileHeader {LastUpdateDate = DateTime.Now};
        outDbaseHeader.AddColumn("ID", 'N', 10, 0);
        outDbaseHeader.AddColumn("test", 'S', 10, 0);
        outDbaseHeader = ShapefileDataWriter.GetHeader(outDbaseHeader.Fields, Math.Max(features.Count, 1));
        dataWriter.Header = outDbaseHeader;

// write the shapefile
        dataWriter.Write(features);

// Create the projection file if necessary, choose value matching srid  
        using (var streamWriter = new StreamWriter(filename+".prj"))
        {
            streamWriter.Write(ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84.WKT);
        }

// delete the temporary writer
        //shpWriter.Close();
        // File.Delete(tmpFile + ".shp");
        // File.Delete(tmpFile + ".shx");
    }
}