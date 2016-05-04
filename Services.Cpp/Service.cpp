// Services.Cpp/Service.cpp
#include "Service.h"

BSTR ConvertMBSToBSTR(const std::string& str)
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

	return ConvertMBSToBSTR(os.str());
}