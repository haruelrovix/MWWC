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
				/// <summary>
				/// Initializes a new instance of the <see cref="Service"/> class.
				/// </summary>
				Service();

				/// <summary>
				/// Finalizes an instance of the <see cref="Service"/> class.
				/// </summary>
				~Service();

				/// <summary>
				/// !s the service.
				/// </summary>
				/// <returns></returns>
				!Service();

				/// <summary>
				/// Gets this instance.
				/// </summary>
				/// <returns></returns>
				String^ Get();

				/// <summary>
				/// Gets the by identifier.
				/// </summary>
				/// <param name="id">The identifier.</param>
				/// <returns></returns>
				String^ GetById(String^ id);

				/// <summary>
				/// Adds the specified string.
				/// </summary>
				/// <param name="str">The string.</param>
				void Add(String^ str);

				/// <summary>
				/// Updates the specified string.
				/// </summary>
				/// <param name="str">The string.</param>
				/// <returns></returns>
				bool Update(String^ str);

				/// <summary>
				/// Deletes the specified identifier.
				/// </summary>
				/// <param name="id">The identifier.</param>
				/// <returns></returns>
				bool Delete(String^ id);

				/// <summary>
				/// Destroys this instance.
				/// </summary>
				void Destroy();

			private:
				/// <summary>
				/// The _impl
				/// </summary>
				Cpp::Service* _impl;
			};
		}
	}
}