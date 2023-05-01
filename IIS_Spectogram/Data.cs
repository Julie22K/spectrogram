using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IIS_Spectogram
{
    internal class Data
    {
        private int rows;
        private int columns;
        private int[,] field;
        public int Rows { get { return rows; } set { rows = value; } }
        public int Columns { get { return columns; } set { columns = value; } }
        public int[,] Field { get { return field; } set { field = value; } }
        public Data(int rows,int columns, int max) {
            this.rows = rows;
            this.columns = columns;
            Fill_field(0, max);
        }
        public Data(int rows, int columns, int min, int max)
        {
            this.rows = rows;
            this.columns = columns;
            Fill_field(min, max);
        }
        public Data(int rows, int columns, int[,] data)
        {
            this.rows = rows;
            this.columns = columns;
            this.field = data;
        }
        public Data(int[] data)
        {
            try { 
                this.rows = 420;
                this.columns = 420;
                int[,] res_data = new int[rows, columns];

                Fill_field(0, 2000);
                /*for(int j=0;j<this.columns ;j++)
                {
                    for (int i = 0; i < this.rows; i++) {
                        if (data[j] == i) res_data[i, j] = 100;
                        else { 
                            res_data[i, j] = 100;
                        } 

                    }
                }*/
            } catch (Exception error) {
                MessageBox.Show(error.ToString());
            }
            
        }
        private void Fill_field(int min, int max) {
            Random r = new Random();
            int[,] new_field = new int[this.rows, this.columns];
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.columns; j++)
                {
                    new_field[i,j] = r.Next(min,max);
                }
            }
            this.field = new_field;
        }
    }
}
