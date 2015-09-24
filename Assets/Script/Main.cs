using System;
using System.IO;
using System.Collections;

namespace FileTransfer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			FileStream fs = new FileStream ("test.dem",FileMode.Create, FileAccess.Write);
			BinaryWriter bw = new BinaryWriter (fs);
			StreamReader sr = new StreamReader ("36814.xyz");
			ArrayList height = new ArrayList ();
			Console.WriteLine ("hello");
			int[] regionMin = new int[2];
			int[] regionMax = new int[2];



			string[] words = (sr.ReadLine()).Split (' ');
			float interval = 90;
			Int32 X = (int)Convert.ToDouble (words [0]);
			Int32 Y = (int)Convert.ToDouble (words [1]);
			height.Add (words [2]);

			regionMax [0] = regionMin [0] = X;
			regionMax [1] = regionMin [1] = Y;


			while (sr.Peek() >= 0) {
				words = (sr.ReadLine()).Split (' ');
				X = (int)Convert.ToDouble (words [0]);
				Y = (int)Convert.ToDouble (words [1]);
				height.Add (words [2]);

				if(regionMax[0]<X)
					regionMax[0] = X;
				if(regionMin[0]>X)
					regionMin[0] = X;

				if(regionMax[1] <Y)
					regionMax[1] = Y;
				if(regionMin[1] > Y)
					regionMin[1] = Y;

			}


			bw.Write (interval);
			bw.Write (regionMin [0]);
			bw.Write (regionMin [1]);
			bw.Write (regionMax [0]);
			bw.Write (regionMax[1]);

			for (int i=0; i<height.Count; i++) {
				bw.Write(Convert.ToDouble(height[i]));
			}

			//bw.Write (Convert.ToDouble(words[0]));
			//bw.Write (Convert.ToDouble(words[1]));


			/*FileStream fs2 = new FileStream ("test.dem", FileMode.Open, FileAccess.Read);
			BinaryReader br = new BinaryReader (fs2);
			Console.WriteLine (br.ReadDouble());
			Console.WriteLine (br.ReadDouble());*/


			sr.Close ();
			bw.Close ();
			fs.Close ();
			
		}
	}
}
