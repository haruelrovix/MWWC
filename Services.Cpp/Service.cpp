// Services.Cpp/Service.cpp
#include "Service.h"

#pragma region Private Methods

BSTR s2bstr(const std::string& str)
{
	int wslen = ::MultiByteToWideChar(CP_ACP, 0 /* no flags */,
		str.data(), str.length(),
		NULL, 0);

	BSTR wsdata = ::SysAllocStringLen(NULL, wslen);
	::MultiByteToWideChar(CP_ACP, 0 /* no flags */,
		str.data(), str.length(),
		wsdata, wslen);

	return wsdata;
}

string ws2s(const std::wstring& wstr)
{
	using convert_typeX = codecvt_utf8<wchar_t>;
	wstring_convert<convert_typeX, wchar_t> converter;

	return converter.to_bytes(wstr);
}

#pragma endregion

BSTR Services::Cpp::Service::Get()
{
	HTTPClientSession session("localhost", 57313);
	HTTPRequest request(HTTPRequest::HTTP_GET, "/EmployeeService.svc/GetAllEmployee/", HTTPMessage::HTTP_1_1);
	request.setHost("localhost", 57313);
	request.setKeepAlive(false);
	session.sendRequest(request);

	HTTPResponse response;
	istream& rs = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(rs, os);

	return s2bstr(os.str());
}

BSTR Services::Cpp::Service::GetById(BSTR id)
{
	wstring ws(id, ::SysStringLen(id));
	string str = ws2s(ws);

	HTTPClientSession session("localhost", 57313);
	HTTPRequest request(HTTPRequest::HTTP_GET, "/EmployeeService.svc/GetEmployeeDetails/" + str, HTTPMessage::HTTP_1_1);
	request.setHost("localhost", 57313);
	request.setKeepAlive(false);
	session.sendRequest(request);

	HTTPResponse response;
	istream& rs = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(rs, os);

	return s2bstr(os.str());
}

void Services::Cpp::Service::Add(BSTR bstr)
{
	HTTPClientSession session("localhost", 57313);
	HTTPRequest request(HTTPRequest::HTTP_POST, "/EmployeeService.svc/AddNewEmployee", HTTPMessage::HTTP_1_1);
	request.setHost("localhost", 57313);
	request.setKeepAlive(false);
	request.setContentType("application/json");

	wstring ws(bstr, ::SysStringLen(bstr));
	string str = ws2s(ws);
	request.setContentLength(str.length());
	session.sendRequest(request) << str;

	HTTPResponse response;
	istream& is = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(is, os);
}

void Services::Cpp::Service::Update(BSTR bstr)
{
	HTTPClientSession session("localhost", 57313);
	HTTPRequest request(HTTPRequest::HTTP_PUT, "/EmployeeService.svc/UpdateEmployee", HTTPMessage::HTTP_1_1);
	request.setHost("localhost", 57313);
	request.setKeepAlive(false);
	request.setContentType("application/json");

	wstring ws(bstr, ::SysStringLen(bstr));
	string str = ws2s(ws);
	request.setContentLength(str.length());
	session.sendRequest(request) << str;

	HTTPResponse response;
	istream& is = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(is, os);
}

void Services::Cpp::Service::Delete(BSTR id)
{
	wstring ws(id, ::SysStringLen(id));
	string str = ws2s(ws);

	HTTPClientSession session("localhost", 57313);
	HTTPRequest request(HTTPRequest::HTTP_DELETE, "/EmployeeService.svc/DeleteEmployee/" + str, HTTPMessage::HTTP_1_1);
	request.setHost("localhost", 57313);
	request.setKeepAlive(false);
	session.sendRequest(request);

	HTTPResponse response;
	istream& rs = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(rs, os);
}