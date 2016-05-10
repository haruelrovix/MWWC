// Services.Cpp/Service.cpp
#include "Resource.h"
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

#pragma region Public Methods

/// <summary>
/// Gets this instance.
/// </summary>
/// <returns></returns>
BSTR Services::Cpp::Service::Get()
{
	HTTPClientSession session(HOSTNAME, PORT);
	string uri = URI + static_cast<string>(REST_GET);
	HTTPRequest request(HTTPRequest::HTTP_GET, uri, HTTPMessage::HTTP_1_1);
	request.setHost(HOSTNAME, PORT);
	request.setKeepAlive(false);
	session.sendRequest(request);

	HTTPResponse response;
	istream& rs = session.receiveResponse(response);

	if (response.getStatus() == HTTPResponse::HTTP_INTERNAL_SERVER_ERROR)
	{
		return s2bstr(response.getReason());
	}

	ostringstream os;
	StreamCopier::copyStream(rs, os);

	return s2bstr(os.str());
}

/// <summary>
/// Gets the by identifier.
/// </summary>
/// <param name="id">The identifier.</param>
/// <returns></returns>
BSTR Services::Cpp::Service::GetById(BSTR id)
{
	wstring ws(id, ::SysStringLen(id));
	string uri = URI + static_cast<string>(REST_GET_BY_ID) + ws2s(ws);

	HTTPClientSession session(HOSTNAME, PORT);
	HTTPRequest request(HTTPRequest::HTTP_GET, uri, HTTPMessage::HTTP_1_1);
	request.setHost(HOSTNAME, PORT);
	request.setKeepAlive(false);
	session.sendRequest(request);

	HTTPResponse response;
	istream& rs = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(rs, os);

	return s2bstr(os.str());
}

/// <summary>
/// Adds the specified BSTR.
/// </summary>
/// <param name="bstr">The BSTR.</param>
void Services::Cpp::Service::Add(BSTR bstr)
{
	HTTPClientSession session(HOSTNAME, PORT);
	string uri = URI + static_cast<string>(REST_POST);
	HTTPRequest request(HTTPRequest::HTTP_POST, uri, HTTPMessage::HTTP_1_1);
	request.setHost(HOSTNAME, PORT);
	request.setKeepAlive(false);
	request.setContentType(JSONP);

	wstring ws(bstr, ::SysStringLen(bstr));
	string str = ws2s(ws);
	request.setContentLength(str.length());
	session.sendRequest(request) << str;

	HTTPResponse response;
	istream& is = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(is, os);
}

/// <summary>
/// Updates the specified BSTR.
/// </summary>
/// <param name="bstr">The BSTR.</param>
/// <returns></returns>
bool Services::Cpp::Service::Update(BSTR bstr)
{
	HTTPClientSession session(HOSTNAME, PORT);
	string uri = URI + static_cast<string>(REST_PUT);
	HTTPRequest request(HTTPRequest::HTTP_PUT, uri, HTTPMessage::HTTP_1_1);
	request.setHost(HOSTNAME, PORT);
	request.setKeepAlive(false);
	request.setContentType(JSONP);

	wstring ws(bstr, ::SysStringLen(bstr));
	string str = ws2s(ws);
	request.setContentLength(str.length());
	session.sendRequest(request) << str;

	HTTPResponse response;
	istream& is = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(is, os);

	return os.str() == TRUE;
}

/// <summary>
/// Deletes the specified identifier.
/// </summary>
/// <param name="id">The identifier.</param>
/// <returns></returns>
bool Services::Cpp::Service::Delete(BSTR id)
{
	wstring ws(id, ::SysStringLen(id));
	string uri = URI + static_cast<string>(REST_DELETE) + ws2s(ws);

	HTTPClientSession session(HOSTNAME, PORT);
	HTTPRequest request(HTTPRequest::HTTP_DELETE, uri, HTTPMessage::HTTP_1_1);
	request.setHost(HOSTNAME, PORT);
	request.setKeepAlive(false);
	session.sendRequest(request);

	HTTPResponse response;
	istream& rs = session.receiveResponse(response);

	ostringstream os;
	StreamCopier::copyStream(rs, os);

	return os.str() == TRUE;
}

#pragma endregion