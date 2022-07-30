using System;

// Variable (변수) :
// 아직 값을 모르는 상징적인 이름 (식별 문자)
int a = 3; // 전역변수(글로벌변수)

namespace Variables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

        }
    }

    class SwordMan
    {
        // 자료형 (Data type) : 
        // 데이터의 종류를 식별하기 위한 분류

        // bit : 한자리 이진수 (0 과 1. 정보처리의 최소단위)
        // byte : 8 bit ( CPU의 데이터 처리 최소단위) 
        // 4 byte == 32 bit

        // 초기화 : 변수 선언과 동시에 값을 넣는것
        uint lv = 1; // int : 4 byte 크기의 부호가 있는 정수 자료형 -2^31 ~ 2^31 - 1
        // uint : unsigend int 4byte 크기의 부호가 없는 정수 자료형 0 ~ 2^32
        float exp; // float : 4byte 크기의 실수
        // double : 8byte 크기의 실수
        bool isAvailable; // bool : 논리형, true 와 false 로 구분함, 1byte
        // true : 0이 아닌 수, false : 0
        char aender; // char : 문자형, 2byte ASCII 코드형식으로 정수형 저장됨
        char[] name1; // 배열 : 연속된 자료형 타입
        String name2 = new String('c', 3); // String : 객체 참조형
        string name; // string : 문자열형

        // 멤버 변수
        // 클래스 내에 선언된 변수
    }
}
