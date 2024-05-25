using BlazorWebAppCamilla.Models;
using GraphQL;
using GraphQL.Client.Http;

namespace BlazorWebAppCamilla.Services;

public class CourseService(GraphQLHttpClient client)
{
    private readonly GraphQLHttpClient _client = client;

    public async Task<Course> GetCourseByIdAsync(string id)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            query ($id: String!) {
                getCourseById(id: $id) {
                    id
                    title
                    imageUri
                    imageHeaderUri
                    isBestseller
                    isDigital
                    categories
                    ingress
                    starRating
                    reviews
                    likes
                    likesInProcent
                    hours
                    authors {
                        name
                        authorImage
                    }
                    prices {
                        currency
                        price
                        discount
                    }
                    content {
                        description
                        includes
                        programDetails {
                            id
                            title
                            description
                        }
                    }
                }
            }",
            Variables = new { id }
        };

        var response = await _client.SendQueryAsync<CourseResponse>(request);
        return response.Data.GetCourseById;
    }

    public async Task<IEnumerable<string>> GetUserCourseIdsAsync(string userId)
    {
        var request = new GraphQLRequest
        {
            Query = @"
        query GetUserCourseIds($userId: String!) {
            getUserCourseIds(userId: $userId)
        }",
            Variables = new { userId }
        };

        var response = await _client.SendQueryAsync<UserCoursesQueryResponse>(request);
        return response.Data.UserCourseIds;
    }

    public async Task<bool> RequestCreateUserCoursesAsync(UserCourses userCourses)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            mutation ($input: UserCoursesInput!) {
                saveUserCourse(input: $input) {
                    userId
                    courseId
                }
            }",
            Variables = new
            {
                input = new
                {
                    UserId = userCourses.UserId,
                    CourseId = userCourses.CourseId
                }
            }
        };

        var response = await _client.SendMutationAsync<UserCoursesResponse>(request);
        return response.Data.SaveUserCourse;
    }

    private class CourseResponse
    {
        public Course GetCourseById { get; set; }
    }

    public class UserCoursesQueryResponse
    {
        public IEnumerable<string> UserCourseIds { get; set; }
    }

    public class UserCoursesResponse
    {
        public bool SaveUserCourse { get; set; }
    }
}
