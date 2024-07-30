using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace VisualLineExtension
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class VisualLineAdornmentTextViewCreationListener : IWpfTextViewCreationListener
    {
#pragma warning disable IDE0051

        [Export(typeof(AdornmentLayerDefinition))]
        [Name(nameof(VisualLineAdornment))]
        [Order(Before = PredefinedAdornmentLayers.Text)]
        private readonly AdornmentLayerDefinition editorAdornmentLayer;

#pragma warning restore IDE0051

        public void TextViewCreated(IWpfTextView textView)
        {
            new VisualLineAdornment(textView);
        }
    }
}
