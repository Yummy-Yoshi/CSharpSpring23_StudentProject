using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class SubmissionService
    {
        private static SubmissionService? _instance;

        public static SubmissionService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SubmissionService();
                }
                return _instance;
            }
        }

        private SubmissionService()
        {

        }
        public void Add(Submission submission)
        {
            FakeDatabase.Submissions.Add(submission);
        }
        public void Remove(Submission submission)
        {
            FakeDatabase.Submissions.Add(submission);
        }
        public Submission? GetById(int id)
        {
            return FakeDatabase.Submissions.FirstOrDefault(s => s.Id == id);
        }
        public IEnumerable<Submission?> Submissions
        {
            get
            {
                return FakeDatabase.Submissions.Where(s => s is Submission);
            }
        }
    }
}
