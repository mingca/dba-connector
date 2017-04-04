
'===========GLOBAL FORM COLLECTION==========================
'PREVENT MORE THAN ONE INSTANCE OF DETAIL FORMS OPENING
'so user doesn't get confused by viewing calls from one contact while another one's detail is open for example
'===========================================================
Public Class ClassOpenForms
    'put forms.add(me) in open method...forms.remove(me) in dispose method
    Private Shared fOrg As frmMainOrg
    Private Shared fCase As frmMainCase
    Private Shared fContact As frmMainContact
    Private Shared fConversation As frmMainConversation
    ' Private Shared fEdEvent As frmMainEdEvent
    Private Shared fWEvent As frmMainWEvent
    ' Private Shared fWorkshop As frmMainWorkshop
    ' Private Shared fNewWorkshop As frmNewWorkshop
    ' Private Shared fWRegistration As frmMainWRegistration
    Private Shared fEventReg2 As frmMainWReg2
    Private Shared fNewWOrder As frmNewWOrder2
    Private Shared fMainWOrder As frmMainWOrder
    Private Shared fMainWPayment As frmMainWPayment
    Private Shared fGeography As frmGeography
    Private Shared fGrant As frmMainGrant
    Private Shared fResource As frmMainResource
    Private Shared fResourceTmp As frmMainResource
    ' Private Shared fResourceOrig As frmMainResourceOrig
    Private Shared fRecommend As frmMainResourceRecommend
    Private Shared fFeedback As frmMainResourceFeedback
    Private Shared fResourceWarning As frmMainResourceWarning
    Private Shared fResourceLocation As frmMainResourceLocation
    Private Shared fResourceRO As frmMainResourceReadOnly
    '  Private Shared fMGI As frmMainMGI
    Private Shared fTMGI As frmMainGrantTMGI   '2011
    Private Shared fYMGI As frmMainGrantYMGI    '2013
    Private Shared fTGI As frmMainGrantTGI '2010
    Private Shared fCMG As frmMainGrantCMG    '2016
    ' Private Shared fReg As frmMainRegistration
    Private Shared fPending As frmMainWPending
    Private Shared fLTGI As frmMainGrantLTGI
    Private Shared vRegistration As rptViewerRegistration    'report viewer for registation reports
    Private Shared vStatsGrant As rptViewerStatsGrant
    Private Shared vResource As rptViewerResource   '
    Private Shared fStory As frmMainOrgStory
    Private Shared fAlert As frmMainOrgAlert
    Private Shared fStaffConv As frmMainStaffConversation
    Private Shared fDGV As frmPopupDatagrid
    Private Shared fMailLblSimple As frmMailLblSimple
    Private Shared fSrchWEvent As frmSrchEvent 'frmsrchWEvent
    Private Shared fSrchOrg As frmSrchOrg
    Private Shared fSrchCase As frmSrchCase


    'to CREATE INSTANCE of org form:
    'dim neworg as new frmmainorg
    'neworg.show()
    'to ACCESS SAME INSTANCE:
    'ClassOpenForms.frmmainorg = neworg
    ' ClassOpenForms.frmmainorg.xxxxx


    Public Shared Property frmMainOrg() As frmMainOrg
        Get
            Return fOrg
        End Get
        Set(ByVal Value As frmMainOrg)
            fOrg = Value
        End Set
    End Property

    Public Shared Property frmMainCase() As frmMainCase
        Get
            Return fCase
        End Get
        Set(ByVal Value As frmMainCase)
            fCase = Value
        End Set
    End Property

    Public Shared Property frmMainContact() As frmMainContact
        Get
            Return fContact
        End Get
        Set(ByVal Value As frmMainContact)
            fContact = Value
        End Set
    End Property

    Public Shared Property frmMainConversation() As frmMainConversation
        Get
            Return fConversation
        End Get
        Set(ByVal Value As frmMainConversation)
            fConversation = Value
        End Set
    End Property

    'Public Shared Property frmMainEdEvent() As frmMainEdEvent
    '    Get
    '        Return fEdEvent
    '    End Get
    '    Set(ByVal Value As frmMainEdEvent)
    '        fEdEvent = Value
    '    End Set
    'End Property

    'Public Shared Property frmWorkshop() As frmMainWorkshop
    '    Get
    '        Return fWorkshop
    '    End Get
    '    Set(ByVal Value As frmMainWorkshop)
    '        fWorkshop = Value
    '    End Set
    'End Property

    'Public Shared Property frmNewWorkshop() As frmNewWorkshop
    '    Get
    '        Return fNewWorkshop
    '    End Get
    '    Set(ByVal Value As frmNewWorkshop)
    '        fNewWorkshop = Value
    '    End Set
    'End Property

    Public Shared Property frmMainWEvent() As frmMainWEvent
        Get
            Return fWEvent
        End Get
        Set(ByVal Value As frmMainWEvent)
            fWEvent = Value
        End Set
    End Property

    'Public Shared Property frmWRegistration() As frmMainWRegistration
    '    Get
    '        Return fWRegistration
    '    End Get
    '    Set(ByVal Value As frmMainWRegistration)
    '        fWRegistration = Value
    '    End Set
    'End Property

    Public Shared Property frmMainWReg2() As frmMainWReg2
        Get
            Return fEventReg2
        End Get
        Set(ByVal Value As frmMainWReg2)
            fEventReg2 = Value
        End Set
    End Property

    Public Shared Property frmNewWOrder() As frmNewWOrder2
        Get
            Return fNewWOrder
        End Get
        Set(ByVal Value As frmNewWOrder2)
            fNewWOrder = Value
        End Set
    End Property

    Public Shared Property frmMainWOrder() As frmMainWOrder
        Get
            Return fMainWOrder
        End Get
        Set(ByVal Value As frmMainWOrder)
            fMainWOrder = Value
        End Set
    End Property

    Public Shared Property frmMainWPayment() As frmMainWPayment
        Get
            Return fMainWPayment
        End Get
        Set(ByVal Value As frmMainWPayment)
            fMainWPayment = Value
        End Set
    End Property


    Public Shared Property frmGeography() As frmGeography
        Get
            Return fGeography
        End Get
        Set(ByVal Value As frmGeography)
            fGeography = Value
        End Set
    End Property

    Public Shared Property frmMainGrant() As frmMainGrant
        Get
            Return fGrant
        End Get
        Set(ByVal Value As frmMainGrant)
            fGrant = Value
        End Set
    End Property

    Public Shared Property frmMainResource() As frmMainResource
        Get
            Return fResource
        End Get
        Set(ByVal Value As frmMainResource)
            fResource = Value
        End Set
    End Property


    Public Shared Property frmMainResourceRO() As frmMainResourceReadOnly
        Get
            Return fResourceRO
        End Get
        Set(ByVal Value As frmMainResourceReadOnly)
            fResourceRO = Value
        End Set
    End Property


    Public Shared Property tmpMainResource() As frmMainResource
        Get
            Return fResourceTmp
        End Get
        Set(ByVal Value As frmMainResource)
            fResourceTmp = Value
        End Set
    End Property

    'Public Shared Property frmMainResourceOrig() As frmMainResourceOrig
    '    Get
    '        Return fResourceOrig
    '    End Get
    '    Set(ByVal Value As frmMainResourceOrig)
    '        fResourceOrig = Value
    '    End Set
    'End Property


    Public Shared Property frmMainResourceRecommend() As frmMainResourceRecommend
        Get
            Return fRecommend
        End Get
        Set(ByVal Value As frmMainResourceRecommend)
            fRecommend = Value
        End Set
    End Property


    Public Shared Property frmMainResourceFeedback() As frmMainResourceFeedback
        Get
            Return fFeedback
        End Get
        Set(ByVal Value As frmMainResourceFeedback)
            fFeedback = Value
        End Set
    End Property

    Public Shared Property frmMainResourceWarning() As frmMainResourceWarning
        Get
            Return fResourceWarning
        End Get
        Set(ByVal Value As frmMainResourceWarning)
            fResourceWarning = Value
        End Set
    End Property


    Public Shared Property frmMainResourceLocation() As frmMainResourceLocation
        Get
            Return fResourceLocation
        End Get
        Set(ByVal Value As frmMainResourceLocation)
            fResourceLocation = Value
        End Set
    End Property

    'Public Shared Property frmMainMGI() As frmMainMGI
    '    Get
    '        Return fMGI
    '    End Get
    '    Set(ByVal Value As frmMainMGI)
    '        fMGI = Value
    '    End Set
    'End Property

    Public Shared Property frmMainYMGI() As frmMainGrantYMGI
        Get
            Return fYMGI
        End Get
        Set(ByVal Value As frmMainGrantYMGI)
            fYMGI = Value
        End Set
    End Property


    Public Shared Property frmMainGrantLTGI() As frmMainGrantLTGI
        Get
            Return fLTGI
        End Get
        Set(ByVal Value As frmMainGrantLTGI)
            fLTGI = Value
        End Set
    End Property


    Public Shared Property frmMainTGI() As frmMainGrantTGI
        Get
            Return fTGI
        End Get
        Set(ByVal Value As frmMainGrantTGI)
            fTGI = Value
        End Set
    End Property


    Public Shared Property frmMainTMGI() As frmMainGrantTMGI
        Get
            Return fTMGI
        End Get
        Set(ByVal Value As frmMainGrantTMGI)
            fTMGI = Value
        End Set
    End Property


    Public Shared Property frmMainCMG() As frmMainGrantCMG
        Get
            Return fCMG
        End Get
        Set(ByVal Value As frmMainGrantCMG)
            fCMG = Value
        End Set
    End Property



    'Public Shared Property frmMainRegistration() As frmMainRegistration
    '    Get
    '        Return fReg
    '    End Get
    '    Set(ByVal Value As frmMainRegistration)
    '        fReg = Value
    '    End Set
    'End Property

    Public Shared Property frmMainOrgStory() As frmMainOrgStory
        Get
            Return fStory
        End Get
        Set(ByVal Value As frmMainOrgStory)
            fStory = Value
        End Set
    End Property

    Public Shared Property frmMainOrgAlert() As frmMainOrgAlert
        Get
            Return fAlert
        End Get
        Set(ByVal Value As frmMainOrgAlert)
            fAlert = Value
        End Set
    End Property


    Public Shared Property rptVwRegistration() As rptViewerRegistration
        Get
            Return vRegistration
        End Get
        Set(ByVal Value As rptViewerRegistration)
            vRegistration = Value
        End Set
    End Property

    Public Shared Property rptVwResource() As rptViewerResource
        Get
            Return vResource
        End Get
        Set(ByVal Value As rptViewerResource)
            vResource = Value
        End Set
    End Property


    Public Shared Property rptVwStats() As rptViewerStatsGrant
        Get
            Return vStatsGrant
        End Get
        Set(ByVal Value As rptViewerStatsGrant)
            vStatsGrant = Value
        End Set
    End Property

    Public Shared Property frmMainStaffConversation() As frmMainStaffConversation
        Get
            Return fStaffConv
        End Get
        Set(ByVal Value As frmMainStaffConversation)
            fStaffConv = Value
        End Set
    End Property


    Public Shared Property frmMailLblSimple() As frmMailLblSimple
        Get
            Return fMailLblSimple
        End Get
        Set(ByVal Value As frmMailLblSimple)
            fMailLblSimple = Value
        End Set
    End Property


    Public Shared Property frmPopupDatagrid() As frmPopupDatagrid
        Get
            Return fDGV
        End Get
        Set(ByVal Value As frmPopupDatagrid)
            fDGV = Value
        End Set
    End Property



    Public Shared Property frmMainWPending() As frmMainWPending
        Get
            Return fPending
        End Get
        Set(ByVal Value As frmMainWPending)
            fPending = Value
        End Set
    End Property

    Public Shared Property frmSrchEvent() As frmSrchEvent 'frmSrchWEvent
        Get
            Return fSrchWEvent
        End Get
        Set(ByVal Value As frmSrchEvent)
            fSrchWEvent = Value
        End Set
    End Property

    Public Shared Property frmSrchOrg() As frmSrchOrg
        Get
            Return fSrchOrg
        End Get
        Set(ByVal Value As frmSrchOrg)
            fSrchOrg = Value
        End Set
    End Property

    Public Shared Property frmSrchCase() As frmSrchCase
        Get
            Return fSrchCase
        End Get
        Set(ByVal Value As frmSrchCase)
            fSrchCase = Value
        End Set
    End Property

    'Public Shared dsOrg As DataSet = frmMainOrg.DsMainOrg1
    'Public Shared dsCase As DataSet = frmMainCase.DsMainCase1
    'Public Shared daOrg As SqlClient.SqlDataAdapter = frmMainOrg.daMainOrg
    'Public Shared daCase As SqlClient.SqlDataAdapter = frmMainCase.daspMainCase
    'Public Shared ctlOrg As Control = frmMainOrg.editOrgName
    'Public Shared ctlCase As Control = frmMainCase.txtCaseName
    'Public Shared parOrg As SqlClient.SqlParameter = frmMainOrg.daMainOrg.SelectCommand.Parameters("@OrgID")
    ''Public paramOrg As SqlClient.SqlDataAdapter.selectcommand.para

    'Shared Property frmSrchEvent As frmSrchEvent


End Class


