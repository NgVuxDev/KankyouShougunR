using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using System.Data;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.DTO
{
    /// <summary>
    /// DTOClass
    /// </summary>
    internal class DTOClass
    {
        /// <summary>
        /// M_SYS_INFO
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// S_NUMBER_DAY
        /// </summary>
        internal S_NUMBER_DAY numberDay;

        /// <summary>
        /// S_NUMBER_YEAR
        /// </summary>
        internal S_NUMBER_YEAR numberYear;

        public DTOClass()
        {
            this.sysInfoEntity = new M_SYS_INFO();
            this.numberDay = new S_NUMBER_DAY();
            this.numberYear = new S_NUMBER_YEAR();
        }

        /// <summary>
        /// Dtoの中身をコピーする
        /// </summary>
        public DTOClass Clone()
        {
            DTOClass returnDto = new DTOClass();
            returnDto.sysInfoEntity = this.sysInfoEntity;
            returnDto.numberDay = this.numberDay;
            returnDto.numberYear = this.numberYear;
            return returnDto;
        }

    }
}
