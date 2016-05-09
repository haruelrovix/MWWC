// Services.Cpp/Service.h
#pragma once

#undef _WINSOCKAPI_
#define _WINSOCKAPI_

#include <codecvt>
#include <iostream>
#include <sstream>
#include <WTypes.h>

#include "Poco/Foundation.h"
#include "Poco/JSON/Parser.h"
#include "Poco/SAX/DefaultHandler.h"
#include "Poco/Net/HTTPClientSession.h"
#include "Poco/Net/HTTPRequest.h"
#include "Poco/Net/HTTPResponse.h"
#include "Poco/StreamCopier.h"

using namespace std;

using namespace Poco;
using namespace Poco::Dynamic;
using namespace Poco::JSON;
using namespace Poco::Net;
using namespace Poco::XML;

namespace Services
{
	namespace Cpp
	{
		class __declspec(dllexport) Service
		{
		public:
			BSTR Get();

			BSTR GetById(BSTR id);

			void Add(BSTR bstr);
		};
	}
}