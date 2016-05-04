#include "Service.h"
#include "../Services.Cpp/Service.h"

// Allocate some memory for the native implementation
Services::Cpp::CLI::Service::Service() : _impl(new Cpp::Service)
{
}

String^ Services::Cpp::CLI::Service::Get()
{
	BSTR val = _impl->Get();
	String^ res = gcnew String(val);
	::SysFreeString(val);

	return res;	// Call native Get
}

void Services::Cpp::CLI::Service::Destroy()
{
	if (_impl != nullptr)
	{
		delete _impl;
		_impl = nullptr;
	}
}

Services::Cpp::CLI::Service::~Service()
{
	// C++ CLI compiler will automatically make all ref classes implement IDisposable.
	// The default implementation will invoke this method + call GC.SuspendFinalize.
	Destroy(); // Clean-up any native resources
}

Services::Cpp::CLI::Service::!Service()
{
	// This is the finalizer
	// It's essentially a fail-safe, and will get called
	// in case Service was not used inside a using block.
	Destroy(); // Clean-up any native resources 
}