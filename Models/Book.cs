using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBM.Models
{
    public class Book
    {
        /// <summary>
        /// 主キー
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 本のタイトル
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 著者
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// 読書開始日
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 読書終了日
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
