//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducateMe4.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuestionType
    {
        public QuestionType()
        {
            this.Questions = new HashSet<Question>();
        }
    
        public int ID { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<Question> Questions { get; set; }
    }
}