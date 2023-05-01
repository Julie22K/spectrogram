using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

namespace test5
{
    internal class Program
    {   
        static void Main(string[] args)
        {

            try {
                string filepath = "hello3";
                byte[] data = File.ReadAllBytes(@"C:/iis/"+ filepath + ".wav");

                //if (BitConverter.IsLittleEndian) Array.Reverse(data);

                int start = 0;
                long end = 10000;//data.LongLength;

                int[] res_int = new int[end];
                string res_str = BitConverter.ToString(data);
                for (int i=start; i<end;i++)
                    res_int[i] = BitConverter.ToInt32(data, i);
                
                
                for (int i = start; i < end; i++)
                {
                    Console.WriteLine($"{res_int[i]}");
                }
                Console.WriteLine(res_str);
                //File.Create(@"C:/iis/" + filepath + "_res.txt").Close();
               // File.WriteAllText(@"C:/iis/"+filepath+"_res.txt", res_str);
                

                //data = (byte[])data.Skip(43);
                /*int h = 1;
                

                string s2 = BitConverter.ToString(data);   // 82-C8-EA-17
                String[] tempAry = s2.Split('-');
                
                byte[] decBytes2 = new byte[tempAry.Length];
                for (int i = 0; i < tempAry.Length; i++)
                    decBytes2[i] = Convert.ToByte(tempAry[i], 16);

                
                foreach (var b in tempAry)
                {
                    Console.WriteLine(b);
                }
                string[] res=new string[end];
                for (long y = start; y < end; y += h)
                {
                    res[y]=Convert.ToString(data[y]);
                }

                int[] res_int = new int[end];
                for (long y = start; y < end; y += h)
                {
                    res_int[y] = Convert.ToInt32(res[y]);
                }

                Console.WriteLine($"Max:{res_int.Max()}\nMin:{res_int.Min()}");

                for (long y = start; y < end; y += h)
                {
                    Console.WriteLine(res[y]);
                }*/
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static bool readWav(string filename, out float[] L, out float[] R)
        {
            L = R = null;

            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);

                    // chunk 0
                    int chunkID = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    int riffType = reader.ReadInt32();


                    // chunk 1
                    int fmtID = reader.ReadInt32();
                    int fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

                    // 16 bytes coming...
                    int fmtCode = reader.ReadInt16();
                    int channels = reader.ReadInt16();
                    int sampleRate = reader.ReadInt32();
                    int byteRate = reader.ReadInt32();
                    int fmtBlockAlign = reader.ReadInt16();
                    int bitDepth = reader.ReadInt16();

                    if (fmtSize == 18)
                    {
                        // Read any extra values
                        int fmtExtraSize = reader.ReadInt16();
                        reader.ReadBytes(fmtExtraSize);
                    }

                    // chunk 2
                    int dataID = reader.ReadInt32();
                    int bytes = reader.ReadInt32();

                    // DATA!
                    byte[] byteArray = reader.ReadBytes(bytes);

                    int bytesForSamp = bitDepth / 8;
                    int nValues = bytes / bytesForSamp;


                    float[] asFloat = null;
                    switch (bitDepth)
                    {
                        case 64:
                            double[]
                                asDouble = new double[nValues];
                            Buffer.BlockCopy(byteArray, 0, asDouble, 0, bytes);
                            asFloat = Array.ConvertAll(asDouble, e => (float)e);
                            break;
                        case 32:
                            asFloat = new float[nValues];
                            Buffer.BlockCopy(byteArray, 0, asFloat, 0, bytes);
                            break;
                        case 16:
                            Int16[]
                                asInt16 = new Int16[nValues];
                            Buffer.BlockCopy(byteArray, 0, asInt16, 0, bytes);
                            asFloat = Array.ConvertAll(asInt16, e => e / (float)(Int16.MaxValue + 1));
                            break;
                        default:
                            return false;
                    }

                    switch (channels)
                    {
                        case 1:
                            L = asFloat;
                            R = null;
                            return true;
                        case 2:
                            // de-interleave
                            int nSamps = nValues / 2;
                            L = new float[nSamps];
                            R = new float[nSamps];
                            for (int s = 0, v = 0; s < nSamps; s++)
                            {
                                L[s] = asFloat[v++];
                                R[s] = asFloat[v++];
                            }
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch
            {
                Console.WriteLine("...Failed to load: " + filename);
                return false;
            }

            return false;
        }
    }
}
