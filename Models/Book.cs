using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBM.Models
{
    /// <summary>
    /// 1冊の本のデータクラス
    /// </summary>
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
        public string? Author { get; set; } = string.Empty;

        /// <summary>
        /// 読書開始日
        /// </summary>
        public DateTime? StartDate { get; set; } = null;

        /// <summary>
        /// 読書終了日
        /// </summary>
        public DateTime? EndDate { get; set; } = null;

        /// <summary>
        /// 総ページ数
        /// </summary>
        public int PageCount { get; set; } = 0;

        /// <summary>
        /// 読書状態（未読・読書中・読了）
        /// </summary>
        public string Status
        {
            get
            {
                if (EndDate != null) return "読了";
                if (StartDate != null) return "読書中";
                else return "未読";
            }
        }
    }
}
