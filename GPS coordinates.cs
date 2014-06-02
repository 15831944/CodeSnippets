// From decimal to DMS
double coord = 59.345235;
int sec = (int)Math.Round(coord * 3600);
int deg = sec / 3600;
sec = Math.Abs(sec % 3600);
int min = sec / 60;
sec %= 60;

public double ConvertDegreeAngleToDouble( double degrees, double minutes, double seconds )
{
    //Decimal degrees = 
    //   whole number of degrees, 
    //   plus minutes divided by 60, 
    //   plus seconds divided by 3600

    return degrees + (minutes/60) + (seconds/3600);
}
