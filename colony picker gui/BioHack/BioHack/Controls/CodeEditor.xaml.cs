﻿using ScintillaNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BioHack.Controls
{
    /// <summary>
    /// Interaction logic for CodeEditor.xaml
    /// </summary>
    public partial class CodeEditor : System.Windows.Controls.UserControl
    {
        public CodeEditor()
        {
            InitializeComponent();

            CodeEditor_Load(null, null);
        }

        internal ScintillaNET.Scintilla TextArea;

        private void CodeEditor_Load(object sender, RoutedEventArgs e)
        {

            // CREATE CONTROL
            TextArea = new ScintillaNET.Scintilla();
            Panel.Child = TextArea;

            // BASIC CONFIG
            TextArea.TextChanged += (this.OnTextChanged);

            // INITIAL VIEW CONFIG
            TextArea.WrapMode = WrapMode.Word;
            TextArea.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();

            // DEFAULT FILE
            //LoadDataFromFile("test.py");

            // INIT HOTKEYS
            InitHotkeys();


        }

        private void InitColors()
        {

            TextArea.SetSelectionBackColor(true, IntToColor(0x114D9C));

        }

        private void InitHotkeys()
        {

            // register the hotkeys with the form
            HotKeyManager.AddHotKey(this, OpenSearch, Key.F, true);
            HotKeyManager.AddHotKey(this, OpenFindDialog, Key.F, true, false, true);
            HotKeyManager.AddHotKey(this, OpenReplaceDialog, Key.R, true);
            HotKeyManager.AddHotKey(this, OpenReplaceDialog, Key.H, true);
            HotKeyManager.AddHotKey(this, Uppercase, Key.U, true);
            HotKeyManager.AddHotKey(this, Lowercase, Key.L, true);
            HotKeyManager.AddHotKey(this, ZoomIn, Key.OemPlus, true);
            HotKeyManager.AddHotKey(this, ZoomOut, Key.OemMinus, true);
            HotKeyManager.AddHotKey(this, ZoomDefault, Key.D0, true);
            HotKeyManager.AddHotKey(this, CloseSearch, Key.Escape);

            // remove conflicting hotkeys from scintilla
            TextArea.ClearCmdKey(Keys.Control | Keys.F);
            TextArea.ClearCmdKey(Keys.Control | Keys.R);
            TextArea.ClearCmdKey(Keys.Control | Keys.H);
            TextArea.ClearCmdKey(Keys.Control | Keys.L);
            TextArea.ClearCmdKey(Keys.Control | Keys.U);

        }

        private void InitSyntaxColoring()
        {

            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 10;
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            TextArea.Styles[ScintillaNET.Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[ScintillaNET.Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[ScintillaNET.Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            TextArea.Styles[ScintillaNET.Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            TextArea.Lexer = Lexer.Python;

            TextArea.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            TextArea.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }

        private void OnTextChanged(object sender, EventArgs e)
        {

        }


        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin()
        {

            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            TextArea.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin()
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {

            TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            TextArea.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            TextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                TextArea.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                TextArea.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            TextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            TextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            TextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion

        #region Drag & Drop File

        public void InitDragDropFile()
        {

            TextArea.AllowDrop = true;
            TextArea.DragEnter += delegate (object sender, System.Windows.Forms.DragEventArgs e)
            {
                if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                    e.Effect = System.Windows.Forms.DragDropEffects.Copy;
                else
                    e.Effect = System.Windows.Forms.DragDropEffects.None;
            };
            TextArea.DragDrop += delegate (object sender, System.Windows.Forms.DragEventArgs e)
            {

                // get file drop
                if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        LoadDataFromFile(path);

                    }
                }
            };

        }

        internal void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {
                //FileName.Text = System.IO.Path.GetFileName(path);
                TextArea.Text = File.ReadAllText(path);
            }
        }

        #endregion

        #region Main Menu Commands

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    LoadDataFromFile(openFileDialog.FileName);
            //}
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSearch();
        }

        private void findDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFindDialog();
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenReplaceDialog();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.SelectAll();
        }

        private void selectLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScintillaNET.Line line = TextArea.Lines[TextArea.CurrentLine];
            TextArea.SetSelection(line.Position + line.Length, line.Position);
        }

        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.SetEmptySelection(0);
        }

        private void indentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Indent();
        }

        private void outdentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Outdent();
        }

        private void uppercaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uppercase();
        }

        private void lowercaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lowercase();
        }

        private void wordWrapToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // toggle word wrap
            //wordWrapItem.Checked = !wordWrapItem.Checked;
            //TextArea.WrapMode = wordWrapItem.Checked ? WrapMode.Word : WrapMode.None;
        }

        private void indentGuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // toggle indent guides
            //indentGuidesItem.Checked = !indentGuidesItem.Checked;
            //TextArea.IndentationGuides = indentGuidesItem.Checked ? IndentView.LookBoth : IndentView.None;
        }

        private void hiddenCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // toggle view whitespace
            //hiddenCharactersItem.Checked = !hiddenCharactersItem.Checked;
            //TextArea.ViewWhitespace = hiddenCharactersItem.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void zoom100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomDefault();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.FoldAll(FoldAction.Contract);
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.FoldAll(FoldAction.Expand);
        }


        #endregion

        #region Uppercase / Lowercase

        private void Lowercase()
        {

            // save the selection
            int start = TextArea.SelectionStart;
            int end = TextArea.SelectionEnd;

            // modify the selected text
            TextArea.ReplaceSelection(TextArea.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            TextArea.SetSelection(start, end);
        }

        private void Uppercase()
        {

            // save the selection
            int start = TextArea.SelectionStart;
            int end = TextArea.SelectionEnd;

            // modify the selected text
            TextArea.ReplaceSelection(TextArea.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            TextArea.SetSelection(start, end);
        }

        #endregion

        #region Indent / Outdent

        private void Indent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to indent,
            // although the indentation function exists. Pressing TAB with the editor focused confirms this.
            GenerateKeystrokes("{TAB}");
        }

        private void Outdent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to outdent,
            // although the indentation function exists. Pressing Shift+Tab with the editor focused confirms this.
            GenerateKeystrokes("+{TAB}");
        }

        private void GenerateKeystrokes(string keys)
        {
            HotKeyManager.Enable = false;
            TextArea.Focus();
            SendKeys.Send(keys);
            HotKeyManager.Enable = true;
        }

        #endregion

        #region Zoom

        private void ZoomIn()
        {
            TextArea.ZoomIn();
        }

        private void ZoomOut()
        {
            TextArea.ZoomOut();
        }

        private void ZoomDefault()
        {
            TextArea.Zoom = 0;
        }


        #endregion

        #region Quick Search Bar

        bool SearchIsOpen = false;

        private void OpenSearch()
        {

            //SearchManager.SearchBox = TxtSearch;
            //SearchManager.TextArea = TextArea;

            //if (!SearchIsOpen)
            //{
            //    SearchIsOpen = true;
            //    InvokeIfNeeded(delegate ()
            //    {
            //        PanelSearch.Visible = true;
            //        TxtSearch.Text = SearchManager.LastSearch;
            //        TxtSearch.Focus();
            //        TxtSearch.SelectAll();
            //    });
            //}
            //else
            //{
            //    InvokeIfNeeded(delegate ()
            //    {
            //        TxtSearch.Focus();
            //        TxtSearch.SelectAll();
            //    });
            //}
        }
        private void CloseSearch()
        {
            //if (SearchIsOpen)
            //{
            //    SearchIsOpen = false;
            //    InvokeIfNeeded(delegate ()
            //    {
            //        PanelSearch.Visible = false;
            //        //CurBrowser.GetBrowser().StopFinding(true);
            //    });
            //}
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            CloseSearch();
        }

        private void BtnPrevSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(false, false);
        }
        private void BtnNextSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(true, false);
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchManager.Find(true, true);
        }

        private void TxtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (HotKeyManager.IsHotkey(e, Key.Enter))
            {
                SearchManager.Find(true, false);
            }
            if (HotKeyManager.IsHotkey(e, Key.Enter, true) || HotKeyManager.IsHotkey(e, Key.Enter, false, true))
            {
                SearchManager.Find(false, false);
            }
        }

        #endregion

        #region Find & Replace Dialog

        private void OpenFindDialog()
        {

        }
        private void OpenReplaceDialog()
        {


        }

        #endregion

        #region Utils

        public static System.Drawing.Color IntToColor(int rgb)
        {
            return System.Drawing.Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {

            action.Invoke();

        }

        #endregion


    }

    internal class HotKeyManager
    {

        public static bool Enable = true;

        public static void AddHotKey(CodeEditor form, Action function, Key key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            form.PreviewKeyDown += delegate (object sender, System.Windows.Input.KeyEventArgs e)
            {
                if (IsHotkey(e, key, ctrl, shift, alt))
                {
                    function();
                }
            };
        }

        public static bool IsHotkey(System.Windows.Input.KeyEventArgs e, Key key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            return e.Key == key && e.KeyboardDevice.Modifiers.HasFlag(ctrl ? ModifierKeys.Control : ModifierKeys.None) &&
               e.KeyboardDevice.Modifiers.HasFlag(shift ? ModifierKeys.Shift : ModifierKeys.None) &&
               e.KeyboardDevice.Modifiers.HasFlag(alt ? ModifierKeys.Alt : ModifierKeys.None);
        }


    }

    internal class SearchManager
    {

        public static ScintillaNET.Scintilla TextArea;
        public static System.Windows.Controls.TextBox SearchBox;

        public static string LastSearch = "";

        public static int LastSearchIndex;

        public static void Find(bool next, bool incremental)
        {
            bool first = LastSearch != SearchBox.Text;

            LastSearch = SearchBox.Text;
            if (LastSearch.Length > 0)
            {

                if (next)
                {

                    // SEARCH FOR THE NEXT OCCURANCE

                    // Search the document at the last search index
                    TextArea.TargetStart = LastSearchIndex - 1;
                    TextArea.TargetEnd = LastSearchIndex + (LastSearch.Length + 1);
                    TextArea.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (!incremental || TextArea.SearchInTarget(LastSearch) == -1)
                    {

                        // Search the document from the caret onwards
                        TextArea.TargetStart = TextArea.CurrentPosition;
                        TextArea.TargetEnd = TextArea.TextLength;
                        TextArea.SearchFlags = SearchFlags.None;

                        // Search, and if not found..
                        if (TextArea.SearchInTarget(LastSearch) == -1)
                        {

                            // Search again from top
                            TextArea.TargetStart = 0;
                            TextArea.TargetEnd = TextArea.TextLength;

                            // Search, and if not found..
                            if (TextArea.SearchInTarget(LastSearch) == -1)
                            {

                                // clear selection and exit
                                TextArea.ClearSelections();
                                return;
                            }
                        }

                    }

                }
                else
                {

                    // SEARCH FOR THE PREVIOUS OCCURANCE

                    // Search the document from the beginning to the caret
                    TextArea.TargetStart = 0;
                    TextArea.TargetEnd = TextArea.CurrentPosition;
                    TextArea.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (TextArea.SearchInTarget(LastSearch) == -1)
                    {

                        // Search again from the caret onwards
                        TextArea.TargetStart = TextArea.CurrentPosition;
                        TextArea.TargetEnd = TextArea.TextLength;

                        // Search, and if not found..
                        if (TextArea.SearchInTarget(LastSearch) == -1)
                        {

                            // clear selection and exit
                            TextArea.ClearSelections();
                            return;
                        }
                    }

                }

                // Select the occurance
                LastSearchIndex = TextArea.TargetStart;
                TextArea.SetSelection(TextArea.TargetEnd, TextArea.TargetStart);
                TextArea.ScrollCaret();

            }

            SearchBox.Focus();
        }


    }
}
