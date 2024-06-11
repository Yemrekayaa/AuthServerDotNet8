using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class ResponseDto<T> where T : class
    {
        public T? Data { get; private set; }
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public ErrorDto? Error { get; private set; }

        public static ResponseDto<T> Success(T data, int statusCode){

            return new ResponseDto<T>{Data = data ,StatusCode = statusCode, IsSuccessful = true};
        }

        public static ResponseDto<T> Success(int statusCode){

            return new ResponseDto<T>{StatusCode = statusCode, IsSuccessful = true};
        }

        public static ResponseDto<T> Fail(ErrorDto errorDto, int statusCode){

            return new ResponseDto<T>{
                StatusCode = statusCode,
                Error = errorDto,
                IsSuccessful = false
            };
        }

        public static ResponseDto<T> Fail(string error, int statusCode, bool isShow){

            var errorDto = new ErrorDto(error, isShow);
            return new ResponseDto<T>{
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
    }
}