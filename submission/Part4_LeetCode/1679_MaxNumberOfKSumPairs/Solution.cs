public class Solution {
    public int MaxOperations(int[] nums, int k) {
        int ans = 0, l = 0, r = nums.Length - 1;
        nums.Sort();
        while (l < r) {
            int sum = nums[l] + nums[r];
            if (sum > k) 
                r--;
            else if (sum < k)
                l++;    
            else {
                r--;
                l++;
                ans++;
            }
        }
        return ans;
    }
}