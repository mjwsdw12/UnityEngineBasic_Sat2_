// VisualStudio : MS 사에서 개발한 IDE (통합 개발 환경)

// 파란색 : 
// 키워드(예약어) 
// 미리 정의되어있는 단어
// 이미 문법용도로 사용되고 있기 때문에 식별용으로 개발자가 쓸 수 없다.

// 흰색 :
// 식별 문자 (이름)

// 청록색 :
// 클래스 타입 식별문자 (이름)

// 하늘색 :
// 함수의 파라미터 (매개변수) 의 이름

// 주황색 :
// 문자 / 문자열 상수

// 글자색이 어둡다면:
// 생략이 가능하다는 뜻 (참조 없다 / 암시적 표현으로 쓸 수 있다)

// 녹색 :
// 주석, 컴파일시 반영되지 않음. (컴파일 : 프로그래밍언어를 다른 언어 ex) 어셈블리어 , 기계어 로 변환하는 과정 )

// using 키워드 : 
// 특정 namespace 를 사용하기 위한 키워드
// ex) System 이라는 namespace 를 사용하겠다
//using System;

// namespace 키워드 :
// 이름으로 공간을 구분하기위한 키워드
// (내부식별자를 가지고 namespace로 묶인 변수, 함수, 클래스, 인터페이스 등등을 구분함) 
namespace FirstProject // Note: actual namespace depends on the project name.
{
    // internal 접근제한자 
    // 동일 어셈블리에서만 접근 가능한 키워드
    
    // class 키워드:
    // 클래스 타입을 정의하기위한 키워드
    internal class Program
    {
        // 함수 
        // 입력을 받아서 연산을 수행하고 출력을 반환함
        //f(x) = ax + b;
        //
        //int f(int x)
        //{
        //    return ax + b;
        //}

        // static 키워드 
        // 정적 키워드, 인스턴스화가 불가능하도록 하는 키워드
        // 인스턴스 : 객체에 실제 값이 할당되었을 경우
        // 인스턴스 ⊂ 객체

        // void 키워드
        // 반환형태 없음
        static void Main(string[] args)
        {
            // . 연산자 
            // 멤버에 접근하는 연산자
            Console.WriteLine("Hello World!");
        }
    }
}