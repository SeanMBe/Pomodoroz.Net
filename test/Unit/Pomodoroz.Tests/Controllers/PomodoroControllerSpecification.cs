using Machine.Specifications;
using Rhino.Mocks;
using WinnipegTomatoes.Controllers;
using WinnipegTomatoes.Models;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace WinnipegTomatoes.Tests.Controllers
{
    public class PomodoroControllerSpecification
    {
        public class When_start_pomodoro_with_valid_activity_id_and_user_name : Observes<PomodoroController>
        {
            private Establish situation = () => {
                                                    pomodoroRepository = depends.on<IPomodoroRepository>();
                                                    expectedActivityId = 55;
                                                    expectedUsername = "some user";
            };

            private Because of = () => sut.StartPomodoro(expectedActivityId, expectedUsername);

            private It should_add_the_expected_pomodoro = () => pomodoroRepository.AssertWasCalled(r => 
                r.AddPomodoro(Arg<Pomodoro>.Matches(p => p.ActivityId == expectedActivityId && p.Username == expectedUsername)));

            private It should_save_the_pomodoro = () => pomodoroRepository.received(r => r.Save());

            private static int expectedActivityId;
            private static string expectedUsername;
            private static IPomodoroRepository pomodoroRepository;
        }

    }
}