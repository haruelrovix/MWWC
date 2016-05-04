#pragma once

#undef _WINSOCKAPI_
#define _WINSOCKAPI_

#include <WTypes.h>
#include <OleAuto.h>

using namespace System;

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

				void Destroy();

			private:
				Cpp::Service* _impl;
			};
		}
	}
}