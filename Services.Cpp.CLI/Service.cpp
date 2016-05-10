#include "Service.h"
#include "../Services.Cpp/Service.h"

/// <summary>
/// Initializes a new instance of the <see cref="Service"/> class.
/// Allocate some memory for the native implementation
/// </summary>
Services::Cpp::CLI::Service::Service() : _impl(new Cpp::Service)
{
}

/// <summary>
/// Gets this instance.
/// </summary>
/// <returns></returns>
String^ Services::Cpp::CLI::Service::Get()
{
	BSTR val = _impl->Get();
	String^ res = gcnew String(val);
	::SysFreeString(val);

	return res;	// Call native Get
}

/// <summary>
/// Gets the by identifier.
/// </summary>
/// <param name="id">The identifier.</param>
/// <returns></returns>
String^ Services::Cpp::CLI::Service::GetById(String^ id)
{
	VARIANT v = { VT_BSTR };
	v.bstrVal = (BSTR)Marshal::StringToBSTR(id).ToPointer();

	BSTR val = _impl->GetById(v.bstrVal);
	String^ res = gcnew String(val);
	::SysFreeString(val);

	return res;	// Call native GetById
}

/// <summary>
/// Adds the specified s.
/// </summary>
/// <param name="s">The s.</param>
void Services::Cpp::CLI::Service::Add(String^ s)
{
	VARIANT v = { VT_BSTR };
	v.bstrVal = (BSTR)Marshal::StringToBSTR(s).ToPointer();

	_impl->Add(v.bstrVal);
}

/// <summary>
/// Updates the specified s.
/// </summary>
/// <param name="s">The s.</param>
/// <returns></returns>
bool Services::Cpp::CLI::Service::Update(String^ s)
{
	VARIANT v = { VT_BSTR };
	v.bstrVal = (BSTR)Marshal::StringToBSTR(s).ToPointer();

	return _impl->Update(v.bstrVal);
}

/// <summary>
/// Deletes the specified identifier.
/// </summary>
/// <param name="id">The identifier.</param>
/// <returns></returns>
bool Services::Cpp::CLI::Service::Delete(String^ id)
{
	VARIANT v = { VT_BSTR };
	v.bstrVal = (BSTR)Marshal::StringToBSTR(id).ToPointer();

	return _impl->Delete(v.bstrVal);
}

/// <summary>
/// Destroys this instance.
/// </summary>
void Services::Cpp::CLI::Service::Destroy()
{
	if (_impl != nullptr)
	{
		delete _impl;
		_impl = nullptr;
	}
}

/// <summary>
/// Finalizes an instance of the <see cref="Service"/> class.
/// </summary>
Services::Cpp::CLI::Service::~Service()
{
	// C++ CLI compiler will automatically make all ref classes implement IDisposable.
	// The default implementation will invoke this method + call GC.SuspendFinalize.
	Destroy(); // Clean-up any native resources
}

/// <summary>
/// !s the service.
/// </summary>
/// <returns></returns>
Services::Cpp::CLI::Service::!Service()
{
	// This is the finalizer
	// It's essentially a fail-safe, and will get called
	// in case Service was not used inside a using block.
	Destroy(); // Clean-up any native resources 
}