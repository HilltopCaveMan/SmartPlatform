using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Basic
{
    public class QuestionInfo
    {
        private bool _isChanged;
        private int _startdata = 0;
        private int _enddata = 0;

        private string questionid = "";
        private string questiontitle = "";
        private string questioncontent = "";
        private string questionman = "";
        private DateTime questiontime = new DateTime();



        public string QuestionId
        {
            get { return questionid; }
            set
            {
                questionid = value;
            }
        }

        public string QuestionTitle
        {
            get { return questiontitle; }
            set {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for QuestionTitle", value, value.ToString());

                _isChanged |= (questiontitle != value); questiontitle = value;
            }
        }

        public string QuestionContent
        {
            get { return questioncontent; }
            set {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for QuestionContent", value, value.ToString());

                _isChanged |= (questioncontent != value); questioncontent = value;
            }
        }

        public string QuestionMan
        {
            get { return questionman; }
            set { questionman = value; }
        }

        public DateTime QuestionTime
        {
            get { return questiontime; }
            set { questiontime = value; }
        }


        public int StartData
        {
            get { return _startdata; }
            set
            {
                _startdata = value;
            }
        }

        public int EndData
        {
            get { return _enddata; }
            set
            {
                _enddata = value;
            }
        }

        #region Public Functions

        /// <summary>
        /// mark the item as deleted
        /// </summary>
        public void MarkAsDeleted()
        {
            _isChanged = true;
        }

        #endregion
    }
}
