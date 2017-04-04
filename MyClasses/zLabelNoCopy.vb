Public Class LabelNoCopy
    'class MyLabel:Label
    '   {
    '   int WM_GETTEXT = 0xD;
    '   int WM_LBUTTONDBLCLK = 0x203;
    '   bool doubleclickflag = false;
    '   protected override void WndProc(ref Message m)
    '   {
    '      if (m.Msg == WM_LBUTTONDBLCLK)
    '      {
    '         doubleclickflag = true;
    '      }
    '      if (m.Msg == WM_GETTEXT && doubleclickflag)
    '      {
    '         doubleclickflag = false;
    '         return;
    '      }
    '      base.WndProc(ref m);
    '   }
    '}

    'http://www.dotnetmonster.com/Uwe/Forum.aspx/winform-controls/5231/Double-click-label-and-its-text-appears-on-the-clipboard
End Class
