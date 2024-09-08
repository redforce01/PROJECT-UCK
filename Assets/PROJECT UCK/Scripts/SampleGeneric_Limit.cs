using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public interface IMyInterface
    {
        void ShowName();
    }

    public class TemplateLimit
    {
        public string limitName;
    }

    public class TemplateLimitChild : TemplateLimit, IMyInterface
    {
        public void ShowName()
        {

        }
    }

    public class SampleTemplate<T> where T : TemplateLimit, IMyInterface
    {
        public string templateName;
        public T templateValue;

        public void ShowName()
        {
            Debug.Log(templateName);
            Debug.Log(templateValue);
        }
    }

    public class SampleGeneric_Limit : MonoBehaviour
    {
        private void Start()
        {
            SampleTemplate<TemplateLimitChild> templateA = new UCK.SampleTemplate<TemplateLimitChild>();
            templateA.templateName = "Template A";
            templateA.templateValue = new TemplateLimitChild();

            SampleTemplate<TemplateLimitChild> templateB = new UCK.SampleTemplate<TemplateLimitChild>();
            templateB.templateName = "Template B";
            templateB.templateValue = new TemplateLimitChild();

            templateA.ShowName();
            templateB.ShowName();
        }
    }
}
