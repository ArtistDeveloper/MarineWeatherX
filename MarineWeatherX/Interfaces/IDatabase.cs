using MarineWeatherX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MarineWeatherX.Interfaces
{
    public interface IDatabase<T>
    {
        // 테이블에 대한 모든 데이터 조회
        List<T>? Get();

        // 테이블에 대해 특정 ID에 해당하는 데이터 조회
        T? GetDetail(int? id);

        void Create(T entity);

        // 테이블에 특정 DATA UPDATE
        void Update(T entity);

        // 테이블에 특정 DATA DELETE
        void Delete(int? id);
    }
}
