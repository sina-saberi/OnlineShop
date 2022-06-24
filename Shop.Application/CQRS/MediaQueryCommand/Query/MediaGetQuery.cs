using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.CQRS.MediaQueryCommand.Query
{
    public record MediaGetQuery(string fileName, string entity) : IRequest<IActionResult>;

    public class MediaGetQueryHandler : IRequestHandler<MediaGetQuery, IActionResult>
    {
        private readonly FileUtility fileUtility;

        public MediaGetQueryHandler(FileUtility fileUtility)
        {
            this.fileUtility = fileUtility;
        }
        public async Task<IActionResult> Handle(MediaGetQuery request, CancellationToken cancellationToken)
        {
            var filePath = fileUtility.GetFileFullPath(request.fileName, request.entity);
            byte[] encryptedData = await File.ReadAllBytesAsync(filePath);
            var decryptedData = fileUtility.DecryptFile(encryptedData);

            return new FileContentResult(decryptedData, "application/txt");
        }
    }

}
