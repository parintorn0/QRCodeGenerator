using System.Text;
using Microsoft.JSInterop;

namespace QRCodeGenerator.Services;

public class BlobService(IJSRuntime js)
{
    public async Task<string> CreateObjectUrlAsync(byte[] data, string mimeType)
    {
        return await js.InvokeAsync<string>("createObjectUrl", data, mimeType);
    }

    public async Task RevokeObjectUrlAsync(string url)
    {
        await js.InvokeVoidAsync("URL.revokeObjectURL", url);
    }

    public string ToDataUrl(byte[] data, string mimeType)
    {
        return $"data:{mimeType};base64,{Convert.ToBase64String(data)}";
    }

    public string ToDataUrl(Stream stream, string mimeType)
    {
        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        return ToDataUrl(ms.ToArray(), mimeType);
    }

    public string SvgToDataUrl(byte[] data)
    {
        return ToDataUrl(data, "image/svg+xml");
    }

    public string SvgToDataUrl(string svgContent)
    {
        var encoded = Uri.EscapeDataString(svgContent);
        return $"data:image/svg+xml,{encoded}";
    }
}