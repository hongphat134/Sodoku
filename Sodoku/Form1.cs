using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sodoku
{
    public partial class Form1 : Form
    {
        private const int SIZE = 3;
        private Button[, , ,] listBtn = new Button[SIZE, SIZE, SIZE, SIZE];

        private List<Posision>[] listArrayOfValue = new List<Posision>[9];
        private List<Posision> listErrorValueHorizontal = new List<Posision>();
        private List<Posision> listErrorValueVertical = new List<Posision>();
        private List<Posision> listErrorValueSubMatrix = new List<Posision>();
        private int[] listCountVertical = new int[9];
        private int[] listCountHorizontal = new int[9];
        private int[] listCountSubMatrix = new int[9];
        bool[] checkVertical = new bool[9];
        bool[] checkHorizontal = new bool[9];
        bool[] checkSubMatrix = new bool[9];
        private const int WIDTH_BUTTON = 80;
        private const int HEIGHT_BUTTON = 60;

        private const int HEIGHT_DISTANCE = 30;
        private const int WIDTH_DISTANCE = 30;

        public void setMapGenerator()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    for (int k = 0; k < SIZE; k++)
                    {
                        for (int m = 0; m < SIZE; m++)
                        {

                            listBtn[i, j, k, m] = new Button()
                            {
                                Size = new Size(WIDTH_BUTTON, HEIGHT_BUTTON),
                                Location = new Point(WIDTH_BUTTON * ((m + (j * SIZE) + 1)) + (HEIGHT_DISTANCE * j) + 150, HEIGHT_BUTTON * ((k + (i * SIZE)) + 1) + (WIDTH_DISTANCE * i)),
                                Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163))),
                                BackColor = Color.White

                            };
                            this.Controls.Add(listBtn[i, j, k, m]);
                        }
                    }
                }
            }
            for (int i = 0; i < listArrayOfValue.Length; i++) listArrayOfValue[i] = new List<Posision>();
        }

        #region Thiết lập giá trị cho các list phụ trợ
        private void setInvalidForSPList(Posision pos)
        {
            listCountHorizontal[(pos.a * 3) + pos.c] += 1;
            listCountVertical[(pos.b * 3) + pos.d] += 1;
            listCountSubMatrix[(pos.a * 3) + pos.b] += 1;
        }
        private void fillInvalid(Posision pos, int caseValue,bool FillColor)
        {
            List<int> listGuestInvalid = new List<int>();
            List<Posision> listGuestPos = new List<Posision>();
            int[] arrayCheck = new int[9];
            //Xử lý ngang => BUG
            if (caseValue == 0)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    for (int m = 0; m < SIZE; m++)
                    {
                        if (listBtn[pos.a, j, pos.c, m].Text.Equals(""))
                        {
                            listGuestPos.Add(new Posision(pos.a, j, pos.c, m));
                        }
                        else
                        {
                            int number = Int16.Parse(listBtn[pos.a, j, pos.c, m].Text) - 1;
                            arrayCheck[number] = 1;
                        }
                    }
                }
            }
            //Xử lý dọc => BUG
            else if (caseValue == 1)
            {
                for (int i = 0; i < SIZE; i++)
                {
                    for (int k = 0; k < SIZE; k++)
                    {
                        if (listBtn[i, pos.b, k, pos.d].Text.Equals(""))
                        {
                            listGuestPos.Add(new Posision(i, pos.b, k, pos.d));
                        }
                        else
                        {
                            int number = Int16.Parse(listBtn[i, pos.b, k, pos.d].Text) - 1;
                            arrayCheck[number] = 1;
                        }
                    }
                }
            }
            //Xử lý ma trận con
            else
            {
                for (int k = 0; k < SIZE; k++)
                {
                    for (int m = 0; m < SIZE; m++)
                    {
                        if (listBtn[pos.a, pos.b, k, m].Text.Equals(""))
                        {
                            listGuestPos.Add(new Posision(pos.a, pos.b, k, m));
                        }
                        else
                        {
                            int number = Int16.Parse(listBtn[pos.a, pos.b, k, m].Text) - 1;
                            arrayCheck[number] = 1;
                        }
                    }
                }
            }
            for (int stt = 0; stt < arrayCheck.Length; stt++)
            {
                if (arrayCheck[stt] == 0) listGuestInvalid.Add(stt + 1);
            }
            if (listGuestPos.Count == 2)
            {
                //||!isDeadLock(listGuestInvalid[0],listGuestPos[0])                       
                if (!isCheckVertical(listGuestInvalid[0], listGuestPos[0]) ||
                            !isCheckHorizontal(listGuestInvalid[0], listGuestPos[0]) ||
                            !isCheckSubMatrix(listGuestInvalid[0], listGuestPos[0])   ) 
                {
                    listBtn[listGuestPos[1].a, listGuestPos[1].b, listGuestPos[1].c, listGuestPos[1].d].Text = listGuestInvalid[0] + "";
                    listBtn[listGuestPos[0].a, listGuestPos[0].b, listGuestPos[0].c, listGuestPos[0].d].Text = listGuestInvalid[1] + "";                 
                }
                else
                {
                    listBtn[listGuestPos[1].a, listGuestPos[1].b, listGuestPos[1].c, listGuestPos[1].d].Text = listGuestInvalid[1] + "";
                    listBtn[listGuestPos[0].a, listGuestPos[0].b, listGuestPos[0].c, listGuestPos[0].d].Text = listGuestInvalid[0] + "";
                }
            }
            else
            {
                listBtn[listGuestPos[0].a, listGuestPos[0].b, listGuestPos[0].c, listGuestPos[0].d].Text = listGuestInvalid[0] + "";
            }
            if (FillColor)
            {
                for (int i = 0; i < listGuestPos.Count; i++) listBtn[listGuestPos[i].a, listGuestPos[i].b, listGuestPos[i].c, listGuestPos[i].d].ForeColor = Color.Red;
            }
            //Check lại các value vừa điền
            for (int i = 0; i < listGuestPos.Count; i++) setInvalidForSPList(new Posision(listGuestPos[i].a, listGuestPos[i].b, listGuestPos[i].c, listGuestPos[i].d));
            for (int i = 0; i < listGuestPos.Count; i++) AutoSetValueMapGenerator(listGuestPos[i],FillColor);
        }

        private void AutoSetValueMapGenerator(Posision pos,bool FillColor)
        {
            if (listCountHorizontal[(pos.a * SIZE) + pos.c] >= 7 && listCountHorizontal[(pos.a * SIZE) + pos.c] != 9) fillInvalid(pos, 0,FillColor);
            if (listCountVertical[(pos.b * SIZE) + pos.d] >= 7 && listCountVertical[(pos.b * SIZE) + pos.d] != 9) fillInvalid(pos, 1, FillColor);
            if (listCountSubMatrix[(pos.a * SIZE) + pos.b] >= 7 && listCountSubMatrix[(pos.a * SIZE) + pos.b] != 9) fillInvalid(pos, 2, FillColor);
        }
        #endregion

        #region Kiểm tra giá trị Sodoku
        private List<Posision> getHorAndVerBaseOnPos(List<Posision> lst, Posision pos)
        {
            List<Posision> listPos = new List<Posision>();
            foreach (Posision vt in lst)
            {
                if (vt.a == pos.a || vt.b == pos.b) listPos.Add(vt);
            }
            return listPos;
        }

        private List<Point> getPosNotInviteBaseOnPos(List<Posision> lstCheck, Posision pos)
        {
            List<Point> listPos = new List<Point>();
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (i != pos.a && j != pos.b)
                    {
                        foreach (Posision vt in lstCheck)
                        {
                            if (vt.a != i && vt.b != j) listPos.Add(new Point(i, j));
                        }
                    }
                }
            }
            return listPos;
        }

        private bool isCheckVertical(int value, Posision pos)
        {
            //Kiểm tra hàng dọc         
            for (int i = 0; i < SIZE; i++)
            {
                for (int k = 0; k < SIZE; k++)
                {
                    if (listBtn[i, pos.b, k, pos.d].Text.Equals(value + "")) return false;
                }
            }
            return true;
        }
        private bool isCheckHorizontal(int value, Posision pos)
        {
            //Kiểm tra hàng ngang
            for (int j = 0; j < SIZE; j++)
            {
                for (int m = 0; m < SIZE; m++)
                {
                    if (listBtn[pos.a, j, pos.c, m].Text.Equals(value + "")) return false;
                }
            }
            return true;
        }
        private bool isCheckSubMatrix(int value, Posision pos)
        {
            for (int k = 0; k < SIZE; k++)
            {
                for (int m = 0; m < SIZE; m++)
                {
                    if (listBtn[pos.a, pos.b, k, m].Text.Equals(value + "")) return false;
                }
            }
            return true;
        }
        private bool isDeadLock(int value, Posision pos)
        {
            List<Posision> array = listArrayOfValue[value - 1];
            if (array.Count >= 2)
            {
                List<Posision> checkArray = getHorAndVerBaseOnPos(array, pos);
                List<Point> checkArrayNotInvite = getPosNotInviteBaseOnPos(checkArray, pos);

                int length;

                foreach (Point ptr in checkArrayNotInvite)
                {
                    length = SIZE;
                    if (ptr.X == pos.a)
                    {
                        for (int k = 0; k < SIZE; k++)
                        {
                            if (k != pos.c)
                            {
                                for (int m = 0; m < SIZE; m++)
                                {
                                    if (!listBtn[ptr.X, ptr.Y, k, m].Text.Equals("")) length++;
                                }
                            }
                        }
                        if (length == 9) return false;
                    }
                    else
                    {
                        for (int k = 0; k < SIZE; k++)
                        {
                            for (int m = 0; m < SIZE; m++)
                            {
                                if (m != pos.d)
                                {
                                    if (!listBtn[ptr.X, ptr.Y, k, m].Text.Equals("")) length++;
                                }
                            }
                        }
                        if (length == 9) return false;
                    }
                }
            }
            return true;
        }
        #endregion

         
        #region Thiết lập giá trị Sodoku
        public int getInvalid(Posision pos)
        {
            Random r = new Random();
            int value;
            do
            {
                value = r.Next(1, 10);
            }while (!isCheckVertical(value, pos) || !isCheckHorizontal(value, pos)  || !isCheckSubMatrix(value, pos) );
            //|| !isDeadLock(value,pos)
            listArrayOfValue[value - 1].Add(pos);
            setInvalidForSPList(pos);
            return value;
        }
        public Posision getPosition()
        {
            Random r = new Random();
            int posA, posB, posC, posD;
            do
            {
                posA = r.Next(0, SIZE);
                posB = r.Next(0, SIZE);
                posC = r.Next(0, SIZE);
                posD = r.Next(0, SIZE);
            } while (!listBtn[posA, posB, posC, posD].Text.Equals(""));
            return new Posision(posA, posB, posC, posD);
        }
        public void setDefaultValue()
        {
            for (int i = 0; i < 42; i++)
            {
                Posision pos = getPosition();
                int value = getInvalid(pos);
                listBtn[pos.a, pos.b, pos.c, pos.d].Text = value + "";
                AutoSetValueMapGenerator(pos,false);
            }
        }
        #endregion

        
        #region AI Sudoku
        // Đủ 7 quân sẽ xét và điền value vào cho đủ
        private void AISudoku()
        {
            sortArrayOfValueDESCBaseOnCount();
            int number = 0;
            while (true)
            {
                if (number == 9) number = 0;
                bool flag = false;
                do
                {
                    flag = false;
                    if (listArrayOfValue[number].Count != 0)
                    {
                        List<Point> listPosNotInvite = getPosNotInviteBaseOnListPos(listArrayOfValue[number]);

                        int value = Int16.Parse(listBtn[listArrayOfValue[number][0].a, listArrayOfValue[number][0].b, listArrayOfValue[number][0].c, listArrayOfValue[number][0].d].Text);
                        foreach (Point point in listPosNotInvite)
                        {
                            List<Posision> listPosToCheckInvalid = getHorAndVerBaseOnPos(listArrayOfValue[number], new Posision(point.X, point.Y, 0, 0));
                            if (isValid(listPosToCheckInvalid, point, value)) flag = true;
                        }
                    }
                } while (flag);
                number++;
                if (isWinner()) break; 
            }
            checkFullMap();


        }
        private void sortArrayOfValueDESCBaseOnCount()
        {
            for (int i = 0; i < listArrayOfValue.Length - 1; i++)
            {
                for (int j = i + 1; j < listArrayOfValue.Length; j++)
                {
                    if (listArrayOfValue[i].Count < listArrayOfValue[j].Count)
                    {
                        List<Posision> temp = listArrayOfValue[i];
                        listArrayOfValue[i] = listArrayOfValue[j];
                        listArrayOfValue[j] = temp;
                    }
                }
            }
        }
        private List<Point> getPosNotInviteBaseOnListPos(List<Posision> lst)
        {
            List<Point> listPoint = new List<Point>();
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    bool flag = true;
                    foreach (Posision pos in lst)
                    {
                        if (i == pos.a && j == pos.b) { flag = false; break; };
                    }
                    if(flag) listPoint.Add(new Point(i,j));
                }
            }
            return listPoint;
        }

        private bool isValid(List<Posision> lst,Point posCompare,int value)
        {
            //Ma trận mô phỏng để check giá trị
            int[,] checkArray = new int[SIZE, SIZE];
          
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (!listBtn[posCompare.X, posCompare.Y, i, j].Text.Equals(""))
                    {
                        checkArray[i, j] = 1;
                    }
                }
            }

            foreach (Posision pos in lst)
            {
                if (pos.a == posCompare.X)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (checkArray[pos.c, j] == 0) checkArray[pos.c, j] = 1;
                    }
                }
                else
                {
                    for (int i = 0; i < SIZE; i++)
                    {
                        if (checkArray[i, pos.d] == 0) checkArray[i, pos.d] = 1;
                    }
                }
            }
            int lenght = 0;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (checkArray[i, j] == 1) ++lenght;
                }
            }
            if (lenght == 8)
            {
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (checkArray[i, j] == 0)
                        {
                            Posision local = new Posision(posCompare.X, posCompare.Y, i, j);
                            listBtn[local.a,local.b,local.c,local.d].Text = value + "";
                            listBtn[local.a,local.b,local.c,local.d].Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
                            listBtn[local.a,local.b,local.c,local.d].ForeColor = Color.Red;

                            lst.Add(local);

                            setInvalidForSPList(local);
                            AutoSetValueMapGenerator(local,true);
                        }
                    }
                }
                return true;
            }
            return false;
        }
        private bool isWinner()
        {
            for (int i = 0; i < listCountVertical.Length; i++)
            {
                if (listCountVertical[i] != 9) return false;
            }
            return true;
        }

        
        #endregion

        #region Try-Catch
        //True là sai, Fail là đúng vì defaultValue của bool là false
        private void checkFullMap()
        {
            listErrorValueHorizontal.Clear(); listErrorValueSubMatrix.Clear(); listErrorValueVertical.Clear();

            for (int i = 0; i < SIZE; i++)
            {
                for(int j = 0 ; j < SIZE ;j++)
                {
                    for(int k = 0 ; k < SIZE ;k++)
                    {
                        for(int m = 0 ; m < SIZE ; m++)
                        {
                            if (listCountSubMatrix[(i * SIZE) + j] == 9 && !checkSubMatrix[(i * SIZE) + j])
                            {
                                //Kiếm tra ma trận con => Đã xong
                                for (int val = (k * SIZE) + m + 1; val < 9; val++)
                                {
                                    if (listBtn[i, j, k, m].Text.Equals(listBtn[i, j, val / SIZE, val % SIZE].Text)) {
                                        listErrorValueSubMatrix.Add(new Posision(i, j, k, m)); 
                                        listErrorValueSubMatrix.Add(new Posision(i, j, val / SIZE, val % SIZE));
                                        checkSubMatrix[(i * SIZE) + j] = true;
                                        break;
                                    }
                                }
                            }
                            if (listCountHorizontal[(i * SIZE) + k] == 9 && !checkHorizontal[(i * SIZE) + k])
                            {
                                //Kiểm tra hàng ngang => Đã xong
                                for (int val = (j * SIZE) + m + 1; val < 9; val++)
                                {
                                    if (listBtn[i, j, k, m].Text.Equals(listBtn[i, val / SIZE, k, val % SIZE].Text)) {
                                        listErrorValueHorizontal.Add(new Posision(i, j, k, m));
                                        listErrorValueHorizontal.Add(new Posision(i, val / SIZE, k, val % SIZE));
                                        checkHorizontal[(i * SIZE) + k] = true; 
                                        break; 
                                    }
                                }
                            }
                            if (listCountVertical[(j * SIZE) + m] == 9 && !checkVertical[(j * SIZE) + m])
                            {
                                //Kiểm tra hàng dọc => Đã xong
                                for (int val = (i * SIZE) + k + 1; val < 9; val++)
                                {
                                    if (listBtn[i, j, k, m].Text.Equals(listBtn[val / SIZE, j, val % SIZE, m].Text)) {
                                        listErrorValueVertical.Add(new Posision(i, j, k, m)); 
                                        listErrorValueVertical.Add(new Posision(val / SIZE, j, val % SIZE, m));
                                        checkVertical[(j * SIZE) + m] = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }                  
                }                                  
            }
        }


        private void OutputTryCatch(bool[] listHorizontal, bool[] listVertical,bool[] listSubMatrix){
            String strVertical = "";
            String strHorizontal = "";
            String strSubMaTrix = "";
            for(int i = 0 ; i <listVertical.Length ; i++){
                strHorizontal += "Hàng ngang " + (i + 1) + ": " + (!listHorizontal[i]?"Đúng":"Sai") + "\n";
                strVertical += "Hàng dọc " + (i + 1) + ": " + (!listVertical[i] ? "Đúng" : "Sai") + "\n";
                strSubMaTrix += "Ma trận con " + (i + 1) + ": " + (!listSubMatrix[i] ? "Đúng" : "Sai") + "\n";
            }
            MessageBox.Show(strHorizontal + " \n" + strVertical + "\n" + strSubMaTrix);
        }


        private void ShowValueByColor(int caseToShow,Color colorSur,Color colorErr)
        {

                for (int value = 0; value < checkHorizontal.Length; value++)
                {
                    if (caseToShow == 0)
                    {
                        if (!checkHorizontal[value])
                        {
                            for (int i = 0; i < SIZE; i++)
                            {
                                for (int j = 0; j < SIZE; j++)
                                {
                                    listBtn[value / SIZE, i, value % SIZE, j].BackColor = colorSur;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < SIZE; i++)
                            {
                                for (int j = 0; j < SIZE; j++)
                                {
                                    listBtn[value / SIZE, i, value % SIZE, j].BackColor = colorErr;
                                }
                            }
                        }
                    }
                    else if (caseToShow == 1)
                    {
                        if (!checkVertical[value])
                        {
                            for (int i = 0; i < SIZE; i++)
                            {
                                for (int j = 0; j < SIZE; j++)
                                {
                                    listBtn[i, value / SIZE, j, value % SIZE].BackColor = colorSur;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < SIZE; i++)
                            {
                                for (int j = 0; j < SIZE; j++)
                                {
                                    listBtn[i, value / SIZE, j, value % SIZE].BackColor = colorErr;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!checkSubMatrix[value])
                        {
                            for (int i = 0; i < SIZE; i++)
                            {
                                for (int j = 0; j < SIZE; j++)
                                {
                                    listBtn[value / SIZE, value % SIZE, i, j].BackColor = colorSur;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < SIZE; i++)
                            {
                                for (int j = 0; j < SIZE; j++)
                                {
                                    listBtn[value / SIZE, value % SIZE, i, j].BackColor = colorErr;
                                }
                            }
                        }
                    }
                }
            }
        
        private void ShowErrorValueByColor(List<Posision> listPos)
        {
            foreach (Posision pos in listPos)
            {
                listBtn[pos.a, pos.b, pos.c, pos.d].BackColor = Color.Yellow;
            }
        }
        private void ClearErrorValueByColor(List<Posision> listPos)
        {
            foreach (Posision pos in listPos)
            {
                listBtn[pos.a, pos.b, pos.c, pos.d].BackColor = Color.White;
            }
        }
        #endregion
        public Form1()
        {
            InitializeComponent();
            setMapGenerator();
            setDefaultValue();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AISudoku();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OutputTryCatch(checkHorizontal, checkVertical, checkSubMatrix);
        }
        private void btnPick_Click(object sender, EventArgs e)
        {
            if (rdnHorizontal.Checked)
            {
                ClearErrorValueByColor(listErrorValueSubMatrix); ClearErrorValueByColor(listErrorValueVertical);
                ShowValueByColor(0,Color.Bisque,Color.Pink);
                ShowErrorValueByColor(listErrorValueHorizontal);

            }
            else if (rdnVertical.Checked)
            {
                ClearErrorValueByColor(listErrorValueSubMatrix); ClearErrorValueByColor(listErrorValueHorizontal);
                ShowValueByColor(1, Color.Bisque, Color.Pink);
                ShowErrorValueByColor(listErrorValueVertical);
               
            }
            else if(rdnSubMatrix.Checked)
            {
                ClearErrorValueByColor(listErrorValueHorizontal); ClearErrorValueByColor(listErrorValueVertical);
                ShowValueByColor(2 , Color.Bisque, Color.Pink);
                ShowErrorValueByColor(listErrorValueSubMatrix);
            }
        }
    }
}
