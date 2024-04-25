using LogicUI.FancyTextRendering;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace LogicUI.FancyTextRendering
{
    [UxmlElement]
    public partial class MarkdownLabel : Label
    {
        private string _MarkdownText;
        private bool _isTextChangesEnabled;
        private MarkdownRenderingSettings _RenderSettings = MarkdownRenderingSettings.Default;

        public MarkdownRenderingSettings RenderSettings
        {
            get => _RenderSettings;
            set
            {
                _RenderSettings = value;
                RefreshProcessedText();
            }
        }

        [CreateProperty]
        [UxmlAttribute]
        [TextArea(minLines: 10, maxLines: 50)]
        public string MarkdownText
        {
            get => _MarkdownText;
            set
            {
                _MarkdownText = value;
                RefreshProcessedText();
            }
        }

        [CreateProperty(ReadOnly = true)]
        public override string text
        {
            get => ((INotifyValueChanged<string>)this).value;
            set
            {
                //We block user from changing the text directly
                if (_isTextChangesEnabled)
                    ((INotifyValueChanged<string>)this).value = value;
            }
        }

        private void RefreshProcessedText()
        {
            _isTextChangesEnabled = true;
            text = Markdown.MarkdownToRichText(_MarkdownText, _RenderSettings);;
            _isTextChangesEnabled = false;
        }
    }
}