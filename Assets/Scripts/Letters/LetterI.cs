namespace Letters
{
    public class LetterI : Letter
    {
        // TODO sometimes you don't have to fix anything
        private void OnMouseDown()
        {
            _particleSystemFix.SetActive(true);

            _letterRepaired();
        }
    }
}