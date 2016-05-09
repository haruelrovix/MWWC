#pragma once

#undef _WINSOCKAPI_
#define _WINSOCKAPI_

#include <WTypes.h>
#include <OleAuto.h>

using namespace System;
using namespace Runtime::InteropServices;

namespace Services
{
	namespace Cpp
	{
		class Service;

		namespace CLI
		{
			public ref class Service
			{
			public:
				Service();
				~Service();
				!Service();

				String^ Get();

				String^ GetById(String^ id);
				
				void Add(String^ str);

				void Delete(String^ id);

				void Destroy();

			private:
				Cpp::Service* _impl;
			};
		}
	}
}