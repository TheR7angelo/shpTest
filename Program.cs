using shpTest.Test;

const int srid = 2154;
const string filename = "test";

Shp.Write(filename, srid);
Shp.Read(filename, srid);


