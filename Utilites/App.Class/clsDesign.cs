using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsDesign
    {
        public void ProgramName(TextBox lblProgramNameTh, TextBox lblProgramNameEn)
        {
            lblProgramNameTh.BackColor = System.Drawing.Color.WhiteSmoke;
            lblProgramNameTh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lblProgramNameTh.Font = new System.Drawing.Font("Mitr ExtraLight", 14F);
            lblProgramNameTh.ForeColor = System.Drawing.Color.DimGray;
            lblProgramNameTh.Location = new System.Drawing.Point(393, 55);
            lblProgramNameTh.Name = "lblProTH";
            lblProgramNameTh.Size = new System.Drawing.Size(322, 30);
            lblProgramNameTh.TabIndex = 0;

            lblProgramNameEn.BackColor = System.Drawing.Color.WhiteSmoke;
            lblProgramNameEn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lblProgramNameEn.Font = new System.Drawing.Font("Josefin Sans", 12F);
            lblProgramNameEn.ForeColor = System.Drawing.Color.IndianRed;
            lblProgramNameEn.Location = new System.Drawing.Point(393, 28);
            lblProgramNameEn.Name = "lblProEN";
            lblProgramNameEn.Size = new System.Drawing.Size(322, 19);
            lblProgramNameEn.TabIndex = 0;
        }

        public void Data(Label lblHeader, Label lblShowData, Label lblList, Label lblDataRow, Label lblCondition, DataGridView dataGridView)
        {
            lblHeader.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            lblHeader.ForeColor = System.Drawing.SystemColors.HotTrack;
            lblHeader.Location = new System.Drawing.Point(16, 19);
            lblHeader.Size = new System.Drawing.Size(53, 20);
            lblHeader.Text = "คำค้นหา : ";

            lblShowData.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            lblShowData.ForeColor = System.Drawing.SystemColors.HotTrack;
            lblShowData.Location = new System.Drawing.Point(1159, 19);
            lblShowData.Size = new System.Drawing.Size(1159, 20);
            lblShowData.Text = "ข้อมูลทั้งหมด";

            lblList.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            lblList.ForeColor = System.Drawing.SystemColors.HotTrack;
            lblList.Location = new System.Drawing.Point(1270, 19);
            lblList.Size = new System.Drawing.Size(53, 20);
            lblList.Text = "รายการ";

            lblDataRow.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            lblDataRow.ForeColor = System.Drawing.SystemColors.InfoText;
            lblDataRow.Location = new System.Drawing.Point(1243, 19);
            lblDataRow.Size = new System.Drawing.Size(53, 20);
            lblDataRow.Text = "0";

            lblCondition.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            lblCondition.ForeColor = System.Drawing.SystemColors.InfoText;
            lblCondition.Location = new System.Drawing.Point(89, 19);
            lblCondition.Size = new System.Drawing.Size(53, 20);
            lblCondition.Text = "-";

            dataGridView.Font = new System.Drawing.Font("Mitr Light", 9.75F);
        }
    }
}