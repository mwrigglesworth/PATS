﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_GIPAPAsync_MainColPatientDiag : System.Web.UI.UserControl
{
    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        iucon.web.Controls.ParameterCollection parameters = iucon.web.Controls.ParameterCollection.Instance;
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            choice = Convert.ToInt32(parameters["choice"]);
        }
        catch
        {
            choice = 0;
        }
        LabelDiagInfo.Text = myPatient.DiagnosisInfoTable(choice,0);
        LabelFinancialInfo.Text = myPatient.FinancialInfoTable(sessUse.Role);
    }
}