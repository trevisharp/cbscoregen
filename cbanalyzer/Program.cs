using System;
using System.IO;

StreamReader reader = new StreamReader("data.csv");
StreamWriter writer = new StreamWriter("cbdata.csv");



reader.Close();
writer.Close();