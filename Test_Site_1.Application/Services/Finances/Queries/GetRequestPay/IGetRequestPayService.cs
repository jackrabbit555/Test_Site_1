﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Finances.Queries.IGetRequestPayService
{
    public interface IGetRequestPayService
    {
        ResultDto<RequestPayDto> Execute(Guid guid);

    }
    public class GetRequestPayService : IGetRequestPayService
    {
        private readonly IDataBaseContext _context;
        public GetRequestPayService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto<RequestPayDto> Execute(Guid guid)
        {
            var requestPay = _context.RequestPays.Where(p => p.Guid == guid).FirstOrDefault();
            if (requestPay != null)
            {
                return new ResultDto<RequestPayDto>()

                {

                    Data = new RequestPayDto()
                    {
                        Amount = requestPay.Amount,
                        Id = requestPay.Id,
                    }
                };
            }
            else 
            {
                throw new Exception("request pay not found");
            }
        }
    }




    public class RequestPayDto
    {
        public int Amount { get; set; }
        public long Id { get; set; }
    }
}
