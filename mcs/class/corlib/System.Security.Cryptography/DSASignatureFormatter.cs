//
// System.Security.Cryptography DSASignatureFormatter.cs
//
// Authors:
//	Thomas Neidhart (tome@sbox.tugraz.at)
//	Sebastien Pouliot (sebastien@ximian.com)
//
// Portions (C) 2002 Motus Technologies Inc. (http://www.motus.com)
// Copyright (C) 2004-2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography {

#if NET_2_0
	[ComVisible (true)]
#endif
	public class DSASignatureFormatter : AsymmetricSignatureFormatter {
	
		private DSA dsa;

		public DSASignatureFormatter () : base ()
		{
		}

		public DSASignatureFormatter (AsymmetricAlgorithm key) : base ()
		{
			SetKey (key);
		}

		public override byte[] CreateSignature (byte[] rgbHash)
		{
			if (dsa == null) {
				throw new CryptographicUnexpectedOperationException (
					Locale.GetText ("missing key"));
			}

			return dsa.CreateSignature (rgbHash);
		}

		public override void SetHashAlgorithm (string strName)
		{
			if (strName == null)
				throw new ArgumentNullException ("strName");

			try {
				// just to test, we don't need the object
				SHA1.Create (strName);
			}
			catch (InvalidCastException) {
				throw new CryptographicUnexpectedOperationException (
					Locale.GetText ("DSA requires SHA1"));
			}
		}

		public override void SetKey (AsymmetricAlgorithm key)
		{
			if (key != null) {
				// this will throw a InvalidCastException if this isn't
				// a DSA keypair
				dsa = (DSA) key;
			}
#if NET_2_0
			else
				throw new ArgumentNullException ("key");
#else
			// null is accepted in 1.0/1.1
#endif
		}
	}
}
