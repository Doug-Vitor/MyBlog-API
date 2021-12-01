using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BetterNews.UnitTests.ApplicationCore.Validations
{
    public class PasswordValidationAttributeTests
    {
        [Fact]
        public void GivenInvalidPasswordsMustReturnFailValidation()
        {
            ValidationContext validationContext = new(new CreateUserInputModel().Password);
        }
    }
}
